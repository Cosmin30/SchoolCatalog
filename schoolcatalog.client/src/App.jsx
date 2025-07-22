import { useEffect, useState } from 'react';
import './App.css';
import WelcomeSection from './components/WelcomeSection/WelcomeSection.jsx';
import Despre from './components/Despre/Despre.jsx';
import Contact from './components/Contact/Contact.jsx';
import Header from './components/Header/Header.jsx';
import Footer from './components/Footer/Footer.jsx';
import { Route,Routes } from 'react-router-dom';
import LoginPage from './components/LoginPage/LoginPage.jsx';
import RegisterPage from './components/RegisterPage/RegisterPage'; 

function App() {
    return (
        <>
        <div className='app-container'>
        <Header/>
        <main className='main-content'>
        <Routes>
        <Route path='/' element={<WelcomeSection/>}/>
        <Route path='/login' element={<LoginPage/>}/>
        <Route path='/register' element={<RegisterPage/>}/>
        <Route path='despre' element={<Despre/>}/>
        <Route path='contact' element={<Contact/>}/>
        </Routes>
        </main>
        <Footer/>
        </div>
        </>
    );
}

export default App;