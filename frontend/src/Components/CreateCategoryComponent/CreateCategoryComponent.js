import React, { useEffect, useState } from "react";
import './CreateCategoryComponent.css';
import { useNavigate } from "react-router";
import {createCategory, getCategories, editActivatedStatus} from '../../Services/categoryService';


export function CreateCategoryComponent(){

    const [categoriesList, setCategoriesList] = useState([]);
    const [name, setName] = useState('');
    const navigate = useNavigate();

    async function handleSubmit(event) {
        event.preventDefault();
        if (name.trim() === '') {
            return;
        }

        try {
            const newCategory = await createCategory(name);
            setCategoriesList([...categoriesList, newCategory]);
            //navigate('/');
        } catch (error) {
            console.error('Error creating category:', error);
        }
    }

    
    async function fetchCategories() {
        try {
            let categories = await getCategories();
            setCategoriesList(categories)
        }
        catch(err){console.log(err.message)}
    }

    useEffect(() => {
        fetchCategories();
    }, []);

    // Function to handle the activation/deactivation of a category
    async function handleActivate(idCategory) {
        await editActivatedStatus(idCategory)

        await fetchCategories();
    }


    return (
        <div className="d-flex flex-column mt-5 justify-content-center align-items-center create-category-container">
            <h2>Create Category {name}</h2>
            <form className="w-40">
                <div className="form-group mt-3">
                    <label>Category Name:</label>
                    <input type="text" id="categoryName" name="categoryName" maxLength={50} className="form-control mt-1" required onChange={(e) => setName(e.target.value)} />
                </div>
                <div className="d-flex mt-5 gap-3 justify-content-center">
                    <button type="submit" className="btn btn-success" onClick={handleSubmit}>Create Category</button>
                    <button type="button" className="btn btn-danger" onClick={() => navigate('/')}>Cancel</button>
                </div>
            </form>

            <table className="table table-striped mt-5 w-25">
                <thead>
                    <tr>
                        <th>Category Name</th>
                        <th>Active</th>
                        <th>Registration Date</th>
                    </tr>
                </thead>
                <tbody>
                    {categoriesList.map((cat) => (
                        <tr key={cat.idCategory}>
                            <td>{cat.name}</td>
                            <td><button className="btn btn-light" onClick={() => handleActivate(cat.idCategory)}>{cat.active ? "✅" : "❌"}</button></td>
                            <td>{cat.registrationDate}</td>
                        </tr>
                    ))}
                </tbody>
            </table>

        </div>
    );
}