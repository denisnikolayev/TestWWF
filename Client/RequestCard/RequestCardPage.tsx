import * as React from "react"
import { Router, Route, Link } from 'react-router'
import * as $ from "jquery";
import {browserHistory} from 'react-router';
import {Search} from "./search";
import {PersonInfo} from "./personInfo";
import {ChooseProduct, IChooseProductModel} from "./chooseProduct";
import {Approve} from "./approve";
import {NewClient} from "./newClient";
import {Revision, IFullCardInfo} from "./revision";

interface IUserTask {
    id: string;
    caption: string;
    queueName: string;
    viewName: string;
    viewInputModel: string;
    wwfId: string;
    buttons: {id:string, name:string, style:string, priority:number}[];
}

export class RequestCardPage extends React.Component<{}, { userTask?: IUserTask, viewModel?:any }> {
    constructor(props:{params?:{id: string}}) {
        super();
        this.state = {viewModel: {} };

        
        if (props && props.params && props.params.id) {
            var id = props.params.id;
            $.get(SERVER_URL + "/api/wwf", { id: id })
                .then((data: IUserTask) => {
                    this.setState({ userTask: data });
                })
                .fail((e) => alert(e));
        } else {
            $.post(SERVER_URL + "/api/wwf/start")
                .then((data: IUserTask) => {
                    this.setState({ userTask: data });
                })
                .fail((e) => alert(e));
        }
    }

    onClick(action: string) {
        var {userTask, viewModel} = this.state;

        $.post(SERVER_URL + "/api/wwf/click", { taskId: userTask.id, buttonId: action, model: JSON.stringify(viewModel)})
            .then((data: IUserTask) => {
                if (data && data.viewName) {
                    this.setState({ userTask: data });
                } else {
                    browserHistory.push("/");
                }
            }).fail((e) =>
                alert(e.responseText)
        );
    }

    wizard() {
        var model = this.state.userTask.viewInputModel ? JSON.parse(this.state.userTask.viewInputModel) : null;

        switch (this.state.userTask.viewName) {
        case "Search":
            return <Search model={model} onChange={m => this.setState({ viewModel: m })} />;
        case "PersonInfo":
            return <PersonInfo model={model} />;
        case "ChooseProduct":
                return <ChooseProduct onChange={m => this.setState({ viewModel: m }) } model={model != null ? (model.product != null ? model.product : model) : null } />;
        case "Approve":
            return <Approve model={model} />;
        case "NewClient":
            return <NewClient onChange={m => this.setState({ viewModel: m }) } model={model} />;
        case "Revision":
            return <Revision onChange={m => this.setState({ viewModel: m }) } model={model} />;

        default:
            return <div>{this.state.userTask.viewName}</div>;
        }
    }


    render() {
        var {userTask} = this.state;
        if (userTask) {
            return (<div className="form">
                <div className="form-body">{this.wizard() }</div>
                <div className="buttons">{userTask.buttons.sort((a,b) => b.priority - a.priority).map(a => <button className={`btn ${a.style}`}  key={a.id} onClick={() => this.onClick(a.id) }>{a.name}</button>) }</div>
            </div>
            );
        } else {
            return (
                <div>
                    <h2>Загрузка....</h2>
               </div>);
        }
      
    }
}