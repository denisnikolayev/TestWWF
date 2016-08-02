import * as React from "react";
import {IPersonInfo} from "./personInfo";
import {ChooseProduct,IChooseProductModel} from "./chooseProduct";
import {NewClient} from "./newClient";

export interface IFullCardInfo {
    person?: IPersonInfo;
    product?: IChooseProductModel;
}

export class Revision extends React.Component<{ onChange: (model: IFullCardInfo) => void, model: IFullCardInfo }, IFullCardInfo> {
    constructor(props) {
        super(props);
        this.state = { person: props.model.person, product: props.model.product };
    }

    onChange(model: IFullCardInfo) {
        this.setState(model, () => this.props.onChange(this.state));
    }

    render() {

        return <div className="row">

            <div className="col-md-6"><NewClient onChange={m => this.onChange({ person: m }) } model={ this.state.person }  /></div>
            <div className="col-md-6"><ChooseProduct onChange={m => this.onChange({ product: m }) } model={ this.state.product }  /></div>
            </div>;
    }
}