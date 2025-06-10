import React, { useEffect, useState } from "react";
import { NoteComponent } from '../NoteComponent/NoteComponent';
import './HomeComponent.css'
import {getNotes, getArchivedNotes, getUnarchivedNotes, getFilteredNotes} from '../../Services/notesService';
import {getCategories} from '../../Services/categoryService';
import { useNavigate } from "react-router";

export function HomeComponent(){
  
  const [notesList, setNotesList] = useState([])
  const [showArchived, setShowArchived] = useState(false);
  const [categories, setCategories] = useState([])

  const [selectedCat, setSelectedCat] = useState(0)
  const [searchTitle, setSearchTitle] = useState("")

  const navigate = useNavigate()

  useEffect(() => {
      async function fetchCategoryData() {
        let categoriesList = await getCategories();
        categoriesList = categoriesList.filter(c => c.active === true)
        setCategories(categoriesList)
      }
      fetchCategoryData();

      fetchUnarchivedNotes();
  }, []);

  async function fetchUnarchivedNotes() {
      try {
        const notes = await getUnarchivedNotes();
        setNotesList(notes)
      }
      catch(err){console.log(err.message)}
    }


  async function fetchArchivedNotes() {
    try {
        const notes = await getArchivedNotes();
        setNotesList(notes)
      }
      catch(err){console.log(err.message)}
  }

  function handleSwitchArchived(event) {
    const checked = event.target.checked;
    setShowArchived(checked);
  }

  function navigateCreateNoteComponent(){
    navigate("/create")
  }

  function navigateCreateCategoryComponent(){
    navigate("/createcategory")
  }

  async function filterNotes() {
    console.log("Filtering notes with category:", parseInt(selectedCat), "and title:", searchTitle, "and archived status:", showArchived);

    const filteredNotes = await getFilteredNotes(parseInt(selectedCat), searchTitle ,showArchived);
    setNotesList(filteredNotes);

    //setShowArchived(false);
    //setSearchTitle("");
    //setSelectedCat(0);
  }

  function resetNotes() {
    setShowArchived(false);
    setSearchTitle("");
    setSelectedCat(0);
    fetchUnarchivedNotes();
  }


  // onArchiveChange={reloadNotes} is passed to NoteComponent to reload the notes when an archive status changes
  return(
      <div className='d-flex flex-column align-items-center justify-content-center m-5 homeContainer'>
          
          <div className='d-flex gap-4 searchContainer'>
            
            <div className="d-flex gap-4 archived-categories-container">
              
              <div className="form-check form-switch d-flex align-items-center gap-5">
              <label className="form-check-label">Archived</label>
              <input className="form-check-input" type="checkbox" checked={showArchived} role="switch" onChange={(e) => handleSwitchArchived(e)}/>
              </div>

              <select className="form-select selectCategoriesInput" defaultValue={0} value={selectedCat} onChange={(e) => setSelectedCat(e.target.value)}>
                <option value={0}>All Categories</option>
                {categories.map(c => <option key={c.idCategory} value={c.idCategory}>{c.name}</option>)}
              </select>
            </div>
            

            <div className="d-flex gap-4 search-reset-container">

              <input type='search' className='form-control' value={searchTitle} placeholder="Search title..." onChange={(e) => setSearchTitle(e.target.value)}></input>

              <button className='btn btn-light searchButton' onClick={filterNotes}>
                <i className="bi bi-search"></i> 
              </button>

              <button className='btn btn-secondary d-flex gap-2 resetButton' onClick={resetNotes}>
                <i className="bi bi-arrow-clockwise"></i> Reset
              </button>
            </div>
        </div>
          
          
          <div className='d-flex mt-5 align-items-center gap-4'>
            <button className='btn btn-success mt-4 mb-4' onClick={navigateCreateNoteComponent}>Create new Note</button>
            <button className='btn btn-primary mt-4 mb-4' onClick={navigateCreateCategoryComponent}>Create new Category</button>
          </div>

         
          
          <main className="d-flex flex-wrap justify-content-center mainNoteContainer">
            {
            notesList.map(n => 
            <NoteComponent key={n.idNote} Id={n.idNote} Title={n.title} Date={n.registrationDate}
             Content={n.textContent} Categories={n.categories} isArchived={n.isArchived} Color={n.color}
       
            onArchiveChange={filterNotes}
             ></NoteComponent>
          )
            }
          </main>
    </div>
  )
}