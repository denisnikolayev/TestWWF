import * as React from "react";
import {ISearchModel} from "./search";

interface IPersonInfo {
    iin: string;
    documentNumber: string;
    fio: string;
    birthDay:string;
}

export class InputPersonInfo extends React.Component<{ model: IPersonInfo}, {}> {
    render() {
        return <div>
            <h2>Данные пользователя</h2>
            <div>ИИН: {this.props.model.iin} </div>
            <div>Номер документа: {this.props.model.documentNumber} </div>
            <div>ФИО: {this.props.model.fio} </div>
            <div>Дата рождения: {this.props.model.birthDay} </div>
        </div>;

    }
}