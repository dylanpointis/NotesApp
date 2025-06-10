#!/bin/bash

echo "Starting application..."

# 1. Start backend

cd backend/NotesAppBackend/NotesAppBackend

#Migrations to the databse
#echo "Creating Database"
#dotnet ef database update

echo "Starting backend..."
dotnet run &
BACK_PID=$!

cd ../../.. 

# 2. Start frontend
echo "Starting frontend..."
cd frontend
npm install
npm start &
FRONT_PID=$!
cd ..


echo "App running. CTRL+C to stop it."
wait $BACK_PID $FRONT_PID
