import * as React from "react";
import {ISearchModel} from "./search";
import {PersonInfo, IPersonInfo} from "./personInfo"
import {IChooseProductModel} from "./chooseProduct";

export interface ICommentForStepModel {
    comment?: string;
}

export class CommentForStep extends React.Component<{ model: ICommentForStepModel  }, {}> {
    constructor() {
        super();
        this.state = { };
    }

    render() {
        return <div className="row">
            <div className="form-group">
                <label>Комментарий: </label>
                <div>{this.props.model != null ? this.props.model.comment : "Eblan"}</div>
            </div>
        </div>;
    }
}