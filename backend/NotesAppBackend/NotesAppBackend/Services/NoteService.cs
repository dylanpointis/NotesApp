using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NotesAppBackend.Models;
using NotesAppBackend.Repositories;
using static Azure.Core.HttpHeader;

namespace NotesAppBackend.Services
{

    public class NoteService
    {
        private NoteAppDBContext _dbContext;
        public NoteService(NoteAppDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        private List<Note> AvoidCyclicalReference(List<Note> receivedList)
        {
            //this is to avoid cyclical references when returning the notes
            foreach (Note note in receivedList)
            {
                foreach (var category in note.Categories)
                {
                    category.Notes = null;
                }
            }
            return receivedList;
        }

        public async Task<List<Note>> GetAllNotes()
        {
            try
            {
                List<Note> notes = await _dbContext.Notes.Include(n => n.Categories).ToListAsync();

                //this is to avoid cyclical references when returning the notes
                notes = AvoidCyclicalReference(notes);

                return notes;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("There was an error retrieving the notes", ex);
            }
        }


        public async Task<List<Note>> GetArchivedNotes()
        {
            try
            {
                List<Note> notes = await _dbContext.Notes.Where(n => n.IsArchived).Include(n => n.Categories).ToListAsync();

                notes = AvoidCyclicalReference(notes);

                return notes;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("There was an error retrieving the archived notes", ex);
            }
        }


        public async Task<List<Note>> GetUnarchivedNotes()
        {
            try
            {
                List<Note> notes = await _dbContext.Notes.Where(n => n.IsArchived == false).Include(n => n.Categories).ToListAsync();

                notes = AvoidCyclicalReference(notes);

                return notes;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("There was an error retrieving the unarchived notes", ex);
            }
        }


        public async Task<bool> CreateNote(Note note)
        {
            try
            {
                //search if there is an already existing note with the same title
                if (await _dbContext.Notes.FirstOrDefaultAsync(n => n.Title == note.Title) != null)
                {
                    return false;
                }


                note.RegistrationDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm");

                //Search the selected categories in the database
                List<Category> cats = new List<Category>();
                foreach (Category category in note.Categories)
                {
                    Category searchedCat = await _dbContext.Categories.FirstOrDefaultAsync(c => c.IdCategory == category.IdCategory);
                    cats.Add(searchedCat);
                }
                note.Categories = cats;



                await _dbContext.Notes.AddAsync(note);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("There was an error creating the note", ex);
            }
        }


        public async Task<bool> DeleteNote(int id)
        {
            Note noteToDelete = await _dbContext.Notes.FirstOrDefaultAsync(n => n.IdNote == id);
            if (noteToDelete != null)
            {
                try
                {
                    _dbContext.Notes.Remove(noteToDelete);
                    await _dbContext.SaveChangesAsync();
                    return true;
                }
                catch (Exception ex) { throw new ApplicationException("There was an error deleting the note", ex); }
            }
            return false;
        }


        //to edit the archived status of a note
        public async Task<bool> EditArchivedStatus(int id, bool isArchived)
        {
            Note noteToEditArchived = await _dbContext.Notes.FirstOrDefaultAsync(n => n.IdNote == id);
            if (noteToEditArchived != null)
            {
                try
                {
                    noteToEditArchived.IsArchived = isArchived;
                    await _dbContext.SaveChangesAsync();

                    return true;
                }
                catch (Exception ex) { throw new ApplicationException("There was an error editing the archived status of the note", ex); }
            }
            return false;
        }



        public async Task<bool> EditNote(Note receivedNote)
        {
            Note currentNoteToEdit = await _dbContext.Notes.Include(n => n.Categories).FirstOrDefaultAsync(n => n.IdNote == receivedNote.IdNote);
            if (currentNoteToEdit != null)
            {
                try
                {
                    currentNoteToEdit.Title = receivedNote.Title;
                    currentNoteToEdit.TextContent = receivedNote.TextContent;
                    currentNoteToEdit.IsArchived = receivedNote.IsArchived;
                    currentNoteToEdit.RegistrationDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm");
                    currentNoteToEdit.Color = receivedNote.Color;


                    List<Category> receivedCategories = receivedNote.Categories.ToList();
                    // Remove categories that are not in the received note
                    foreach (Category cat in currentNoteToEdit.Categories)
                    {
                        //delete the category if it is not in the received note
                        if (receivedCategories.Any(rc => rc.IdCategory == cat.IdCategory) == false)
                        {
                            currentNoteToEdit.Categories.Remove(cat);
                        }
                    }
                    // Add new categories from the received note
                    foreach (Category category in receivedNote.Categories)
                    {
                        // search the category in the database
                        Category searchedCat = await _dbContext.Categories.FirstOrDefaultAsync(c => c.IdCategory == category.IdCategory);
                        if (searchedCat != null && currentNoteToEdit.Categories.Any(c => c.IdCategory == searchedCat.IdCategory) == false)
                        {
                            currentNoteToEdit.Categories.Add(searchedCat);
                        }
                    }


                    await _dbContext.SaveChangesAsync();

                    return true;
                }
                catch (Exception ex) { throw new ApplicationException("There was an error editing the note", ex); }
            }
            return false;
        }

        public async Task<List<Note>> GetFilteredNotes(int categoryId, string? title, bool? isArchived)
        {
            try
            {
                if (categoryId == 0 && string.IsNullOrEmpty(title) && isArchived == null)
                {
                    // If no filters are applied, return all notes
                    return await GetAllNotes();
                }

                //filter the notes based on the provided filters

                List<Note> filteredNotes;

                // first filter by IsArchived status
                if (isArchived == null)
                {
                    filteredNotes = await GetAllNotes();
                }
                else if (isArchived == true)
                {
                    filteredNotes = await GetArchivedNotes();
                }
                else
                {
                    filteredNotes = await GetUnarchivedNotes();
                }

                // second filter by categoryId if needed
                if (categoryId > 0)
                {
                    filteredNotes = filteredNotes.Where(n => n.Categories.Any(c => c.IdCategory == categoryId)).ToList();
                }

                // third filter by title if needed
                if (string.IsNullOrEmpty(title) == false)
                {
                    title = title.ToLower();
                    filteredNotes = filteredNotes.Where(n => n.Title.ToLower().Contains(title)).ToList();
                }


                return filteredNotes;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("There was an error retrieving the filtered notes", ex);
            }
        }

        //    public async Task<List<Note>> GetNotesByCategory(int id)
        //    {
        //        try
        //        {
        //            //Filter the notes whose categories matches with the specified id
        //            List<Note> notesByCategoryList = await _dbContext.Notes
        //            .Where(n => n.Categories.Any(c => c.IdCategory == id)) //the any method selectes the categories that match the specified id
        //            .Include(n => n.Categories)
        //            .ToListAsync();

        //            //This is to avoid cyclical references when returning the notes
        //            foreach (Note note in notesByCategoryList)
        //            {
        //                foreach (var category in note.Categories)
        //                {
        //                    category.Notes = null;
        //                }

        //            }

        //            return notesByCategoryList;
        //        }
        //        catch (Exception ex)
        //        {
        //            throw new ApplicationException("There was an error retrieving the notes by category", ex);
        //        }

        //    }
        //}
    }
}
