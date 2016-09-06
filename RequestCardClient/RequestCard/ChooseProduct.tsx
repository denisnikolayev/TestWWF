import * as React from "react";
import {ISearchModel} from "./search";
import {IPersonInfo} from "./personInfo";
import {CommentForStep, ICommentForStepModel} from "./commentForStep";

export interface IChooseProductModel {
    curriency?: string;
    name?:string;
}

export class ChooseProduct extends React.Component<{ onChange: (model: IChooseProductModel) => void, model: { product: IChooseProductModel, commentForStep: ICommentForStepModel} }, IChooseProductModel> {
    constructor(props) {
        super(props);
        this.state = props.model.product != null ? { curriency: props.model.product.curriency, name: props.model.product.name } : {};
    }

    componentDidMount() {
        this.onChange(this.state);
    }

    onChange(model: IChooseProductModel) {
        this.setState(model, () => this.props.onChange(this.state));
    }


    render() {
        return <form>
            <h2 style={{ borderBottom: "1px solid black" }}>Подбор продукта</h2>
            <div className="form-group">
                <label>Валюта: </label>
                <select style={{ width: "200px" }} maxLength={12} className="form-control"  value={this.state.curriency}
                    onChange={(e: any) => this.onChange({ curriency: e.target.value }) } >
                        <option value=""></option>
                        <option value="Тенге">Тенге</option>
                        <option value="USD">USD</option>
                        <option value="Рубли">Рубли</option>
                    </select>
            </div>

            <div className="form-group">
                <label>Тип карточки: </label>
                <select style={{ width: "200px" }} maxLength={12} className="form-control"  value={this.state.name}
                    onChange={(e: any) => this.onChange({ name: e.target.value }) } >
                    <option value=""></option>
                    <option value="Visa Electron Instant">Visa Electron Instant</option>
                    <option value="Visa Platinum">Visa Platinum</option>
                    <option value="Present Card">Present Card</option>
                </select>
            </div>
            <div className={this.props.model.commentForStep != null ? 'form-group' : 'hidden'}>
                <CommentForStep model={ this.props.model.commentForStep } />
            </div>
        </form>;

    }
}