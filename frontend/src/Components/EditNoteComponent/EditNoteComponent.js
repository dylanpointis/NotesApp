import React, {useState, useEffect} from 'react';
import './EditNoteComponent.css';
import { useLocation, useNavigate } from "react-router";
import {getCategories} from '../../Services/categoryService';
import {editNote} from '../../Services/notesService';

export function EditNoteComponent(){
    const location = useLocation()    
    const navigate = useNavigate()
    const { Id, Title, Content, Categories, isArchived, Color } = location.state || {};

    const [titleState, setTitleState] = useState(Title);
    const [contentState, setContentState] = useState(Content);
    const [color, setColor] = useState(Color || "#fbf549"); // Default color is yellow

    
    const [categories, setCategories] = useState([])
    const [selectedCatList, setSelectedCatList] = useState(Categories);
    const [currentSelectedCat, setCurrentSelectedCat] = useState("")
    const [errorMessage, setErrorMessage] = useState("")


    useEffect(() => {
        async function fetchData() {
            try {
            let categoriesList = await getCategories();
            categoriesList = categoriesList.filter(c => c.active === true)
            setCategories(categoriesList)
            }
            catch(err){console.log(err.message)}
        }
        fetchData()
        }, []);


    function handleSelect(){
    if(currentSelectedCat !== null && currentSelectedCat !== ""){
        if(selectedCatList.find(c => c.idCategory === parseInt(currentSelectedCat)) === undefined){
            // If the category is not already selected, add it to the list
            const selected = categories.find(c => c.idCategory === parseInt(currentSelectedCat));
            setSelectedCatList([...selectedCatList, selected])
        }
    }
    }

    function handleRemoveCategory(option){
        const updatedList = selectedCatList.filter(c => c.idCategory !== parseInt(option));
        setSelectedCatList(updatedList);
    }


    function validateFields(){
        if(titleState.trim().length <= 0){
            setErrorMessage("Complete the title field")
            return false;
        }

        if(contentState.trim().length <= 0){
            setErrorMessage("Complete the content field")
            return false;
        }

        if(selectedCatList.length === 0){
            setErrorMessage("Select at least one category")
            return false;
        }
        
        setErrorMessage("")
        return true
    }


    async function handleSubmit(e){
        e.preventDefault()
        if(validateFields()){
            const note = {
                IdNote: Id,
                Title: titleState,
                TextContent: contentState,
                RegistrationDate: "",
                IsArchived: isArchived,
                Color: color,
                Categories: selectedCatList.map(cat => ({
                    IdCategory: cat.idCategory,
                    Name: cat.name,
                    Active: cat.active,
                    RegistrationDate: cat.registrationDate,
                    Notes: []
                }))
            }

            try {
                // Call the API to update the note
                const data = await editNote(note);
                setErrorMessage('');
                navigate('/');
            } catch (error) {
                setErrorMessage(error.message);
            }
        }
    }


    return(
        <div className='d-flex flex-column align-items-center justify-content-center editContainer'>
            <h1 className='mt-5'>Edit Note: {titleState}</h1>
            <form className="editForm">
                <div className="form-group mt-3 editFormGroup">
                    <label>Title</label>
                    <input type="text" id="titleTextBox" className="form-control" maxLength={100} defaultValue={Title}  onChange={e => setTitleState(e.target.value.trim())}></input>
                </div>
                <div className="form-group mt-3 editFormGroup">
                    <label>Content</label>
                    <textarea className="form-control" id="contentTextBox" maxLength={3000} defaultValue={Content} onChange={e => setContentState(e.target.value)}></textarea>
                </div>
                
                <div className="form-group mt-3 editFormGroup">
                    <label>Categories</label>
                    <select className="form-select" onChange={(e) => setCurrentSelectedCat(e.target.value)}>
                        
                        <option value="">-Select a category</option>
                        {categories.map(c => <option key={c.idCategory} value={c.idCategory}>{c.name}</option>)}
                    </select>
                    <button className="btn btn-primary mt-2" type="button" onClick={handleSelect}>Add category</button>
                </div>

                <div className="mt-3 selectedCategoriesContainer">
                    {selectedCatList.map(c => 
                        <button key={c.idCategory} value={c.idCategory} type="button" className="btn btn-light" onClick={(e) => handleRemoveCategory(e.currentTarget.value)}>
                            {c.name} <i className="bi bi-x"></i>
                        </button>
                    )}
                </div>

                <div className="form-group mt-3 mb-5">
                    <label>Color</label>
                    <select className="form-select colorSelection" value={color} onChange={(e) => setColor(e.target.value)}>
                        <option value="#fbf549">Yellow</option>
                        <option value="#f04444">Red</option>
                        <option value="#9fd0ec">Light blue</option>
                        <option value="#fca5ec">Pink</option>
                        <option value="#7de8aa">Green</option>
                        <option value="#ffffff">White</option>
                    </select>
                </div>



                <p className="text-danger">{errorMessage}</p>
                <div className="d-flex justify-content-spacearound mt-4 gap-2">
                    <button className="btn btn-success" type="submit" onClick={handleSubmit}>Edit</button>
                    <button className="btn btn-danger" onClick={() => navigate('/')}>Cancel</button>
                </div>

            </form>
        </div>
    );
}