import React from 'react';
import { connect } from 'react-redux';
import s from '../HomePage.module.css';
import { withRouter } from 'react-router-dom';
import { compose } from 'redux';
import { setCurrentText, setCurrentKey } from '../../../redux/textarea-reducer.js';
import { postFile } from '../../../redux/app-reducer.js';


const CryptForm = (props) => {
    let getText = React.createRef();
    let getOutputText = React.createRef();
    let getKey = React.createRef();
    let outputText = props.outputText;
    let onTextChange = () => {
        
        let textContent = getText.current.value;
        props.setCurrentText(textContent);
    };
    let onInputChange = () => {

        let input = getKey.current.value;
        props.setCurrentKey(input);
    };
    let onDecrypt = (e) => {
        
        props.setInitialised();
        e.preventDefault();
    }
    let onSave = (e) => {
        props.saveFile();
        e.preventDefault();
    }
    return (
        
        <form onSubmit={onDecrypt } className={s.Form_wrapper}>
            <textarea spellCheck="false" ref={getText} onChange={onTextChange} placeholder="Введите текст для расшифровки" className={s.Form_wrapper__textarea} value={props.currentText} />
            <input spellCheck="false" ref={getKey} onChange={onInputChange} type="text" placeholder="Введите ключ" className={s.Form_wrapper__input} value={props.currentDecryptKey} />
            <textarea spellCheck="false" readOnly={true} ref={getOutputText} placeholder="Расшифрованный текст здесь" className={s.Form_wrapper__textarea + " " + s.textarea__output} value={outputText} />
            <div className={s.Form_wrapper__buttons}>
                <button className={s.filepicker_button} type="submit">Расшифровать</button>
                <button onClick={onSave} className={s.filepicker_button}>Сохранить результат</button>
            </div>
            
        </form>
    );

}

class DecryptFormClass extends React.Component {
    constructor(props) {
        super(props);
        this.setCurrentText = this.setCurrentText.bind(this);
        this.setCurrentKey = this.setCurrentKey.bind(this);
        this.setInitialised = this.setInitialised.bind(this);
        this.saveFile = this.saveFile.bind(this);
    }
    
    
    setCurrentText(textContent) {
      
        let text = textContent;
        this.props.setCurrentText(text);
    }
    setCurrentKey(inputContent) {
        let key = inputContent;
        this.props.setCurrentKey(key);
    }

    async setInitialised() {
        let text = this.props.textareaReducer.currentText;
        let key = this.props.textareaReducer.currentKey;
        let title = this.props.appReducer.title;
        await this.props.postFile(text, key, title, false);
    }
    saveFile() {
        this.props.saveFile();
    }
    render() {
        return (<CryptForm outputText={this.props.appReducer.decrypted}
            currentText={this.props.textareaReducer.currentText} currentDecryptKey={this.props.textareaReducer.currentKey}
            setInitialised={this.setInitialised} setCurrentText={this.setCurrentText} setCurrentKey={this.setCurrentKey}
            saveFile={this.saveFile}
        />)
    }
}
let mapStateToProps = (state) => ({

    appReducer: state.appInit,
    textareaReducer: state.textareaReducer
});

export default compose(connect(mapStateToProps, { postFile, setCurrentText, setCurrentKey })
    , withRouter,
)(DecryptFormClass);