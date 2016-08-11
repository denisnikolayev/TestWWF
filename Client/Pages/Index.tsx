import * as React from "react"
import { Router, Route, Link } from 'react-router'

interface IQueue {
    name: string;
    tasks: { caption:string, number:number, id:string}[];
}
interface ICanceled {
    name: string;
    infoCanceled: { caption: string, commentText: number}[];
}

export class Index extends React.Component<{}, { queues: IQueue[], canceled: ICanceled[]}> {
   
    constructor() {
        super();
        this.state = {queues:[], canceled:[]};

        $.get(SERVER_URL + "/api/wwf/queues")
            .then((data: IQueue[]) => {
                var queuesItem = data;
                $.get(SERVER_URL + "/api/wwf/taskCanceled")
                    .then((data: ICanceled[]) => {
                        this.setState({ queues: queuesItem, canceled: data});
                    }).fail((e) => alert(e));
            }).fail((e) => alert(e));
    }


    render() {

        var body = this.state.queues.map(q =>
            <div key={q.name}>
                <h3>{q.name}</h3>
                <ul className="list-unstyled">
                    {q.tasks.map(t => <li> <Link to={`/RequestCard/${t.id}`}>{t.caption} №{t.number}</Link></li>)}
                </ul>
             </div>
        );

        var taskCanceled = this.state.canceled.map(q =>
            <div key={q.name}>
                <h3>{q.name}</h3>
                <ul className="list-unstyled">
                    {q.infoCanceled.map(t => <li> <div>{t.caption}, Комментарий: {t.commentText}</div></li>) }
                </ul>
            </div>
        );

        return (
            <div>
                <Link  className="btn  btn-primary" to="/RequestCard">Создать Заявку на выпуск карты</Link>
                {body}  
                {taskCanceled} 
            </div>
            );
    }
}