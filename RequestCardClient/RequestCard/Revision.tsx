import * as React from "react";
import {IPersonInfo} from "./personInfo";
import {ChooseProduct,IChooseProductModel} from "./chooseProduct";
import {NewClient} from "./newClient";
import {ICommentForStepModel} from "./commentForStep";

export interface IFullCardInfo {
    person?: IPersonInfo;
    product?: IChooseProductModel;
    commentForStep?: ICommentForStepModel;
}

export class Revision extends React.Component<{ onChange: (model: IFullCardInfo) => void, model: IFullCardInfo }, IFullCardInfo> {
    constructor(props) {
        super(props);
        this.state = { person: props.model.person, product: props.model.product, commentForStep: props.model.commentForStep };
    }

    onChange(model: IFullCardInfo) {
        this.setState(model, () => this.props.onChange(this.state));
    }

    render() {
        var modelForClient = { person: this.state.person, commentForStep: this.state.commentForStep };
        var modelForProduct = { product: this.state.product, commentForStep: this.state.commentForStep };

        return <div className="row">

            <div className="col-md-6"><NewClient onChange={m => this.onChange({ person: m }) } model={ modelForClient }  /></div>
            <div className="col-md-6"><ChooseProduct onChange={m => this.onChange({ product: m }) } model={ modelForProduct }  /></div>
            </div>;
    }
}