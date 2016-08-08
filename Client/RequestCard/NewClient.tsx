import * as React from "react";
import {ISearchModel} from "./search";
import {IPersonInfo} from "./personInfo"
import {CommentForStep,ICommentForStepModel} from "./commentForStep";

export class NewClient extends React.Component<{ onChange: (model: IPersonInfo) => void, model: { person: IPersonInfo, commentForStep: ICommentForStepModel} }, IPersonInfo> {
    constructor(props) {
        super(props);
        this.state = props.model.person != null ?
            { iin: props.model.person.iin, documentNumber: props.model.person.documentNumber, fio: props.model.person.fio, birthDay: props.model.person.birthDay } :
            {};
    }

    onChange(model: IPersonInfo) {
        this.setState(model, () => this.props.onChange(this.state));
    }

    render() {
        return <form>
            <h2 style={{ borderBottom: "1px solid black" }}>Данные клиента</h2>

            <div className="form-group">
                <label>ИИН: </label>
                <input style={{ width: "200px" }} maxLength={12} className="form-control" type="text" value={this.state.iin}
                    onChange={(e: any) => this.onChange({ iin: e.target.value }) }/>
            </div>

            <div className="form-group">
                <label>Номер документа: </label>
               
                <input style={{ width: "200px" }} maxLength={12} className="form-control" type="text" value={this.state.documentNumber}
                    onChange={(e: any) => this.onChange({ documentNumber: e.target.value }) } />
            </div>

            <div className="form-group">
                <label>ФИО: </label>

                <input style={{ width: "300px" }} maxLength={36} className="form-control" type="text" value={this.state.fio}
                    onChange={(e: any) => this.onChange({ fio: e.target.value }) } />
            </div>

            <div className="form-group">
                <label>Дата рождения: </label>

                <input style={{ width: "150px" }} maxLength={12} className="form-control" type="date" value={this.state.birthDay}
                    onChange={(e: any) => this.onChange({ birthDay: e.target.value }) } />
            </div>
            <div className={this.props.model.commentForStep != null ? 'form-group' : 'hidden'}>
                <CommentForStep model={ this.props.model.commentForStep } />
            </div>
        </form>;

    }
}