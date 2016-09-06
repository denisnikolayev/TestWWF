import * as React from "react";
import {ISearchModel} from "./search";
import {PersonInfo, IPersonInfo} from "./personInfo"
import {IChooseProductModel} from "./chooseProduct";

export class Approve extends React.Component<{ model: { person: IPersonInfo, product: IChooseProductModel} }, {}> {
    chooseProductInfo() {
        let { product } = this.props.model;

        return <div className="col-md-6">
                    <h2 style={{ borderBottom: "1px solid black" }}>Выбранный продукт</h2>

                    <div className="form-group">
                        <label>Валюта: </label>
                        <p className="form-control-static">{product.curriency}</p>
                    </div>

                    <div className="form-group">
                        <label>Карта: </label>
                        <p className="form-control-static">{product.name}</p>
                    </div>
            </div>;
    }

    render() {
        return <div className="row">
                <div className="col-md-6"><PersonInfo model={this.props.model.person} /></div>
                {this.chooseProductInfo()}
            </div>;

    }
}