import {React, useState} from "react";
import { useLocation, useNavigate } from "react-router";
import './DeleteNoteComponent.css'
import { deleteNote } from "../../Services/notesService";

export function DeleteNoteComponent(){

    const location = useLocation()    
    const navigate = useNavigate()
    const [errorMessage, setErrorMessage] = useState("");
    
    const { Id, Title } = location.state || {};
    
    async function handleDelete(e){
        try{
            e.preventDefault()
            await deleteNote(Id)
            navigate("/")
        }
        catch(err){
            setErrorMessage(err.message)
        }
    }


    if(Id != null && Title != null){
        return(
                <form className="d-flex flex-column justify-content-center align-items-center delete-form" >
                    <h1 className="text-danger">Are you sure you want to delete the note: "{Title}"?</h1>
                    <div>
                        <button className="btn btn-light" onClick={handleDelete}>Yes</button>
                        <button className="btn btn-light" type="button" onClick={() => navigate("/")}>No</button>
                    </div>
                </form>
            )
    }
    else{
        return(<h3>Note not received</h3>)
    }
    
}