/*import React from 'react';
import s from '../HomePage.module.css';
import { FilePicker } from 'react-file-picker';

const Filepicker = (props) => {
    let handleFileChange = (file) => {
        props.handleFileChange(file);
       
    }
    return (

        <div className={s.Filepicker_wrapper}>
            <FilePicker
                extensions={['docx', 'txt']}
                //   onChange={FileObject => ()}
                onChange={handleFileChange}
                onError={errMsg => console.log(errMsg)}
            >
                <button className={s.filepicker_button}>Загрузить</button>
            </FilePicker>

        </div>

    )
}
export default Filepicker;*/
import React from 'react'
import { post } from 'axios';
import s from '../HomePage.module.css';

class UploadForm extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            id: 'get-id-from-somewhere',
            file: null,
        };
    }

    async submit(e) {
        e.preventDefault();

        const url = `http://target-url/api/${this.state.id}`;
        const formData = new FormData();
        formData.append('body', this.state.file);
        const config = {
            headers: {
                'content-type': 'multipart/form-data',
            },
        };
        return post(url, formData, config);
    }

    setFile(e) {
        this.setState({ file: e.target.files[0] });
    }

    render() {
        return (
            <form className={s.Filepicker_wrapper} onSubmit={e => this.submit(e)}>
                <input className={s.filepicker_input} type="file" onChange={e => this.setFile(e)} />
                <button className={s.filepicker_button} type="submit">Загрузить</button>
            </form>
        );
    }
}
export default UploadForm;