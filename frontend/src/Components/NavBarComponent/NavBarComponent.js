
import React from "react";
import { useNavigate } from "react-router";
import './NavBarComponent.css'

export function NavBarComponent(){
    const navigate = useNavigate();
    return(
        <nav className='w-100 bg-danger p-4 text-white navbarcontainer'>
            <button className="btn d-flex align-items-center gap-3" onClick={() => {navigate('/'); window.location.reload();}}>
                <img src="Icons/Notes_icon.png" className="logoImg"></img>
                <h3>NotesApp</h3>
            </button>
        </nav>
    )   
}
