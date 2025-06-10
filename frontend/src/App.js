import './App.css';
import 'bootstrap/dist/css/bootstrap.min.css';
import 'bootstrap-icons/font/bootstrap-icons.css';
import { HomeComponent } from './Components/HomeComponent/HomeComponent';
import { CreateNoteComponent } from './Components/CreateNoteComponent/CreateNoteComponent';
import { NavBarComponent } from './Components/NavBarComponent/NavBarComponent';
import { BrowserRouter, Routes, Route } from 'react-router';
import { DeleteNoteComponent } from './Components/DeleteNoteComponent/DeleteNoteComponent';
import { CreateCategoryComponent } from './Components/CreateCategoryComponent/CreateCategoryComponent';
import { EditNoteComponent } from './Components/EditNoteComponent/EditNoteComponent';


function App() {
  return(
        <BrowserRouter>
      <div className='app-container'>
        <NavBarComponent />
        <Routes>
          <Route path='/' element={<HomeComponent />} />
          <Route path='/create' element={<CreateNoteComponent />} />
          <Route path='/delete' element={<DeleteNoteComponent />} />
          <Route path='/edit' element={<EditNoteComponent />} />
          <Route path='/createcategory' element={<CreateCategoryComponent />} />
        </Routes>
        <footer className='bg-danger mt-5 text-white'>
          <p>Dylan Pointis</p>
        </footer>
      </div>
    </BrowserRouter>
  );
}


export default App;