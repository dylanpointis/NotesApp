import {React, useEffect, useState} from "react";
import './NoteComponent.css'
import { useNavigate } from "react-router";
import { editArchivedStatus} from "../../Services/notesService";


export function NoteComponent({Id,Title, Date, Content, Categories, isArchived, Color, onArchiveChange}){
    const navigate = useNavigate()
    const [isArchivedState, setIsArchivedState] = useState(isArchived);

    
    async function handleArchive() 
    {
        // Toggle the archived state. Copying the current state and inverting it.
        const newValue = !isArchivedState;
        setIsArchivedState(newValue);

        // Call the service to update the archived status in the backend.
        try{
            await editArchivedStatus(Id, newValue);
            if (onArchiveChange) {
                onArchiveChange(); //Call the parent function to reload notes
            }

        }
        catch(err){
            console.log(err.message);
        }
    }

    return(
        <div className="p-3 m-2 rounded noteComponent" style={{ backgroundColor: Color }}>
            <div className="d-flex align-items-center justify-content-md-center p-0">
                <button className="btn actionButton" onClick={() => navigate("/edit", {state: {Id, Title, Content, Categories, isArchived, Color}})}>
                    <i className="bi bi-pen-fill"></i>
                </button>
                <button className="btn actionButton" onClick={() => navigate("/delete", {state: {Id, Title}})}>
                    <i className="bi bi-trash-fill"></i>
                </button>
                <button className="btn actionButton" style={{ color: isArchivedState ? 'green' : 'black' }} onClick={handleArchive}>
                    <i className="bi bi-archive-fill"></i>
                </button>
            </div>
            <h5 className="noteTitle">{Title}</h5>
            <p className="noteDate">{Date}</p>
            <hr></hr>
            <div  className="noteContent">{Content}</div>
            
            <hr></hr>
            <div className="categoryList">
                {Categories.map(c => <span key={c.idCategory}>{c.name} </span>)}
            </div>
        </div>
    )
}