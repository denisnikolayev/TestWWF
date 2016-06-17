import * as React from "react"
import { Router, Route, Link } from 'react-router'
import * as $ from "jquery";
import {browserHistory} from 'react-router';
import {Search} from "./search";
import {InputPersonInfo} from "./inputPersonInfo";

interface IUserTask {
    id: string;
    caption: string;
    queueName: string;
    viewName: string;
    viewInputModel: string;
    wwfId: string;
    buttons: {id:string, name:string}[];
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
        switch (this.state.userTask.viewName) {
        case "Search":
            return <Search onChange={(model)=>this.setState({viewModel:model})} />;
        case "InputPersonInfo":
            return <InputPersonInfo model={JSON.parse(this.state.userTask.viewInputModel) } />;

        default: return <div>{this.state.userTask.viewName}</div>;
        }
    }


    render() {
        var {userTask} = this.state;
        if (userTask) {
            return (<div className="form">
                <div className="form-body">{this.wizard() }</div>
                <div className="buttons">{userTask.buttons.map(a => <button className="btn btn-default"  key={a.id} onClick={() => this.onClick(a.id) }>{a.name}</button>) }</div>
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