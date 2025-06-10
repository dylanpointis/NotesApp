const API_URL = 'http://localhost:5072/api';

export async function getNotes() {

  const response = await fetch(`${API_URL}/notes`);
  return handleResponse(response);
}


export async function getArchivedNotes() {

  const response = await fetch(`${API_URL}/notes/archived`);
  
   if(!response.ok){ 
     const errorBody = await response.json();
    throw new Error(`HTTP Error: ${response.status} ${errorBody.message}`)
  }
  const data = await response.json()
  return data;
}


export async function getUnarchivedNotes() {

  const response = await fetch(`${API_URL}/notes/unarchived`);
  return handleResponse(response);
}



export async function  createNote(note) {
  const response = await fetch(`${API_URL}/notes`, {
    method: 'post', 
    headers: {"Content-Type": "application/json"},
    body: JSON.stringify(note)
  })


  return handleResponse(response);
}


export async function deleteNote(id) {
  const response = await fetch(`${API_URL}/notes/${id}`, {
    method: 'delete', 
    headers: {"Content-Type": "application/json"}
  })

  return handleResponse(response);
}




export async function editArchivedStatus(id, isArchived) {
  const response = await fetch(`${API_URL}/notes/${id}/archive`, {
    method: 'put', 
    headers: {"Content-Type": "application/json"},
    body: JSON.stringify(isArchived ) //Send the new archived status, not an object, just the boolean value
  })
  
  if(!response.ok){ 
      const errorBody = await response.json();
      throw new Error(`HTTP Error: ${response.status} ${errorBody.message}`)
    }
}


export async function editNote(note) {
  const response = await fetch(`${API_URL}/notes`, {
    method: 'put', 
    headers: {"Content-Type": "application/json"},
    body: JSON.stringify(note)
  })

  return handleResponse(response);
}



export async function getFilteredNotes(categoryId, searchTitle, showArchived) {
  const response = await fetch(`${API_URL}/notes/filter?categoryId=${categoryId}&title=${searchTitle}&isArchived=${showArchived}`);
  return handleResponse(response);
}


async function handleResponse(response) {
  if (!response.ok) {
    const errorBody = await response.json();
    throw new Error(`HTTP Error: ${response.status} ${errorBody.message}`);
  }
  return await response.json();
}