import React from 'react';
import s from './HomePage.module.css';
import Filepicker from './FilePicker/FilePicker.jsx';
import DecryptForm from './CryptForm/DecryptForm.jsx';
import EncryptForm from './CryptForm/EncryptForm.jsx';

const HomePage = (props) => {
    let appState = props.appReducer;
    let params = props.params;
    
    return (
       
        <div className={s.homepage_wrapper}>
            <h1 className={s.header_title_common}>Добро пожаловать!</h1>
            {(!params || params === "Decrypt") ?
                <p className={s.header_text_common}>Чтобы воспользоваться расшифровкой - введите исходный текст в поле ниже, а затем укажите ключ расшифровки</p> :
                <p className={s.header_text_common}>Чтобы воспользоваться шифрованием - введите исходный текст в поле ниже, а затем укажите ключ шифрования</p>}
           

            {(!params || params === "Decrypt") ? < DecryptForm saveFile={props.saveFile} params={params} /> : <  EncryptForm saveFile={props.saveFile} />}
            <div className={s.header_text_common}>
                {!appState.title ? "Загрузите файл с помощью кнопки ниже. Принимаются только файлы с расширением docx или txt." : appState.title}
            </div>
            <Filepicker setInit={props.setInit} handleFileChange={props.handleFileChange} extractWordRawText={props.extractWordRawText} />
          
        </div>
        
    );
   

}
export default HomePage;