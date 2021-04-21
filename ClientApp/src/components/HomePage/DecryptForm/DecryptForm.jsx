import React from 'react';
import { connect } from 'react-redux';
import s from '../HomePage.module.css';
const DecryptForm = (props) => {
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
        <form className={s.Form_wrapper}>
            <textarea ref={getText} onChange={onTextChange} placeholder="Введите текст для расшифровки" className={s.Form_wrapper__textarea} value={props.currentText} />
            <input ref={getKey} onChange={onInputChange} type="text" placeholder="Введите ключ" className={s.Form_wrapper__input} value={props.currentDecryptKey} />
            <textarea readOnly={ true} ref={getOutputText} placeholder="Расшифрованный текст здесь" className={s.Form_wrapper__textarea} value={outputText} />
            <button className={s.filepicker_button} onClick={onDecrypt} type="submit">Расшифровать</button>
        </form>
    );

}
class DecryptFormClass extends React.Component {
    constructor(props) {
        super(props);
        this.state = { currentText: "", currentDecryptKey: "" ,outputText:""};
        this.setCurrentText = this.setCurrentText.bind(this);
        this.setCurrentKey = this.setCurrentKey.bind(this);
        this.setInitialised = this.setInitialised.bind(this);
    }
    
    
    setCurrentText(textContent) {
        let text = textContent;
        this.setState({
            currentText: text
        });
    }
   
    componentDidMount() {
        this.state.outputText = this.props.appReducer.text;
    }
    
    componentDidUpdate() {
        
        
    }
    setCurrentKey(inputContent) {
        let key = inputContent;
        this.setState({
            currentDecryptKey: key
        });
    }
    async setInitialised() {
        
        await this.props.setInit();
        this.state.outputText = this.props.appReducer.text; 
        
    }
    render() {
        return (<DecryptForm outputText={this.state.outputText} setInitialised={this.setInitialised} setCurrentText={this.setCurrentText} currentText={this.state.currentText} setCurrentKey={this.setCurrentKey} currentDecryptKey={this.state.currentDecryptKey} />
            )
    }
}
let mapStateToProps = (state) => ({

    appReducer: state.appInit
});
export default connect(mapStateToProps, {})(DecryptFormClass);

