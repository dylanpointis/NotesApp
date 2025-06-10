using Microsoft.EntityFrameworkCore;
using NotesAppBackend.Models;
using NotesAppBackend.Repositories;

namespace NotesAppBackend.Services
{
    public class CategoryService
    {
        private NoteAppDBContext _dbContext;
        public CategoryService(NoteAppDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Category>> getAllCategories()
        {
            try
            {
                List<Category> categories = await _dbContext.Categories.ToListAsync();
                return categories;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("There was an error retrieving the categories", ex);
            }
        }

        public async Task<bool> createCategory(Category category)
        {
            try
            {
                Category searchedCategory = await _dbContext.Categories.FirstOrDefaultAsync(c => c.Name == category.Name);
                if (searchedCategory != null)
                {
                    return false;
                }


                category.RegistrationDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm");
                category.Active = true;
                _dbContext.Categories.Add(category);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("There was an error creating the category", ex);
            }
        }

        //Inverts the Active status of a category received by idCategory
        public async Task<bool> editActivatedStatus(int idCategory)
        {
            try
            {
                Category category = await _dbContext.Categories.FindAsync(idCategory);
                if (category == null)
                {
                    return false;
                }

                // Invert the Active status of the category
                category.Active = !category.Active;

                _dbContext.Categories.Update(category);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("There was an error updating the category status", ex);
            }
        }
    }
}
