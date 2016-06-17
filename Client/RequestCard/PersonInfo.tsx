import * as React from "react";
import {ISearchModel} from "./search";

export interface IPersonInfo {
    iin?: string;
    documentNumber?: string;
    fio?: string;
    birthDay?:string;
}

export class PersonInfo extends React.Component<{ model: IPersonInfo}, {}> {
    render() {
        return <form>
            <h2 style={{borderBottom:"1px solid black"}}>Данные клиента</h2>

            <div className="form-group">
                <label>ИИН:</label>
                <p className="form-control-static">{this.props.model.iin}</p>
            </div>

            <div className="form-group">
                <label>Номер документа: </label>
                <p className="form-control-static">{this.props.model.documentNumber}</p>
            </div>

            <div className="form-group">
                <label>ФИО: </label>
                <p className="form-control-static">{this.props.model.fio}</p>
            </div>

            <div className="form-group">
                <label>Дата рождения: </label>
                <p className="form-control-static">{this.props.model.birthDay}</p>
            </div>
        </form>;

    }
}