import React from 'react';
import s from './HomePage.module.css';
import Filepicker from './FilePicker/FilePicker.jsx';
import DecryptForm from './CryptForm/DecryptForm.jsx';
const HomePage = (props) => {
    let appState = props.appReducer;
    return (
       
        <div className={s.homepage_wrapper }>
            <h1 className={s.header_title_common}>Добро пожаловать!</h1>
            <p className={s.header_text_common}>Чтобы воспользоваться расшифровкой - введите исходный текст в поле ниже, а затем укажите ключ расшифровки</p>
            <DecryptForm setInit={props.setInit} />
            <div>
                {!appState.title ? "Загрузите файл с помощью кнопки ниже. Принимаются только файлы с расширением docx или txt." : appState.title}
            </div>
            <Filepicker  setInit={props.setInit} handleFileChange={props.handleFileChange} extractWordRawText={props.extractWordRawText}/>
        </div>
        
    );
   

}
export default HomePage;