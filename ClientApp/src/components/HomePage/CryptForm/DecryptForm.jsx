import React from 'react';
import { connect } from 'react-redux';
import s from '../HomePage.module.css';
import { withRouter } from 'react-router-dom';
import { compose } from 'redux';
import { setCurrentText, setCurrentKey } from '../../../redux/textarea-reducer.js';


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
    return (
        
        <form onSubmit={onDecrypt } className={s.Form_wrapper}>
            <textarea spellcheck="false" ref={getText} onChange={onTextChange} placeholder="Введите текст для расшифровки" className={s.Form_wrapper__textarea} value={props.currentText} />
            <input spellcheck="false" ref={getKey} onChange={onInputChange} type="text" placeholder="Введите ключ" className={s.Form_wrapper__input} value={props.currentDecryptKey} />
            <textarea spellcheck="false" readOnly={true} ref={getOutputText} placeholder="Расшифрованный текст здесь" className={s.Form_wrapper__textarea + " " + s.textarea__output} value={outputText} />
            <button className={s.filepicker_button} type="submit">Расшифровать</button>
        </form>
    );

}

class DecryptFormClass extends React.Component {
    constructor(props) {
        super(props);
        this.setCurrentText = this.setCurrentText.bind(this);
        this.setCurrentKey = this.setCurrentKey.bind(this);
        this.setInitialised = this.setInitialised.bind(this);
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
        await this.props.setInit(text, key, title,false);
    }
    
    render() {
        return (<CryptForm outputText={this.props.appReducer.decrypted}
            currentText={this.props.textareaReducer.currentText} currentDecryptKey={this.props.textareaReducer.currentKey}
            setInitialised={this.setInitialised} setCurrentText={this.setCurrentText} setCurrentKey={this.setCurrentKey} />)
    }
}
let mapStateToProps = (state) => ({

    appReducer: state.appInit,
    textareaReducer: state.textareaReducer
});

export default compose(connect(mapStateToProps, { setCurrentText, setCurrentKey })
    , withRouter,
)(DecryptFormClass);