const API_URL = 'http://localhost:5072/api';

export async function getCategories() {

  const response = await fetch(`${API_URL}/categories`);
  if (!response.ok) {
    throw new Error('Error fetching categories');
  }
  const data = await response.json()
  //console.log(data)
  return data;
}

export async function createCategory(name) {

  const category = {
    IdCatgory: 0,
    Name: name,
    Active: true,
    RegistrationDate: "",
    Notes: []
  }

  const response = await fetch(`${API_URL}/categories`, {
    method: 'POST',
    headers: {
      'Content-Type': 'application/json'
    },
    body: JSON.stringify(category)
  });

  
  return handleResponse(response);
}



export async function editActivatedStatus(idCategory) {
  const response = await fetch(`${API_URL}/categories/editActivatedStatus/${idCategory}`, {
    method: 'PUT',
    headers: {
      'Content-Type': 'application/json'
    }
  });
  
  return handleResponse(response);
}


async function handleResponse(response) {
  if (!response.ok) {
    const errorBody = await response.json();
    throw new Error(`HTTP Error: ${response.status} ${errorBody.message}`);
  }
  return await response.json();
}