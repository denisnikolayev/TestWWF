import * as React from "react";
import {ISearchModel} from "./search";

export class InputPersonInfo extends React.Component<{ model: ISearchModel}, {}> {
    render() {
        return <div>
            <h2>Данные пользователя</h2>
            <div>ИИН: {this.props.model.iin} </div>
            <div>Номер документа: {this.props.model.documentNumber} </div>
        </div>;

    }
}