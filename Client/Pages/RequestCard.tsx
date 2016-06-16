import * as React from "react"
import { Router, Route, Link } from 'react-router'
import * as $ from "jquery";
import {browserHistory} from 'react-router';

interface IUserTask {
    id: string;
    caption: string;
    queueName: string;
    viewName: string;
    viewInputName: string;
    wwfId: string;
    buttons: {id:string, name:string}[];
}

export class RequestCard extends React.Component<{}, { userTask?: IUserTask }> {
    constructor() {
        super();
        this.state = {};

        //TODO: открытие таски

        $.post(SERVER_URL + "/api/wwf/start")
            .then((data: IUserTask) => {
                this.setState({ userTask: data });
            });

        //TODO: сделать несколько view с полями
        //TODO: получение модели и передача её во вью
        //TODO: при нажатии на кнопки отправлять model
    }

    onClick(action: string) {
        var {userTask} = this.state;

        $.post(SERVER_URL + "/api/wwf/click", { taskId: userTask.id, buttonId: action })
            .then((data: IUserTask) => {
                if (data && data.viewName) {
                    this.setState({ userTask: data });
                    
                } else {
                    browserHistory.push("/");
                }
            });
    }

    render() {
        var {userTask} = this.state;
        if (userTask) {
            return (<div>
                <h1>Add</h1>
                <h3>{userTask.viewName}</h3>
                <div>{userTask.buttons.map(a => <button key={a.id} onClick={() => this.onClick(a.id) }>{a.name}</button>) }</div>
                <pre>{JSON.stringify(userTask, undefined, " ")}</pre>
            </div>
            );
        } else {
            return (
                <div>
                    <h1>Add</h1>
                    <h2>Wait</h2>
               </div>);
        }
      
    }
}