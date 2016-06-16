import * as React from "react"
import { Router, Route, Link } from 'react-router'

interface IQueue {
    name: string;
    tasks: { caption:string, number:number, id:string}[];
}

export class Index extends React.Component<{}, { queues: IQueue[]}> {
   
    constructor() {
        super();
        this.state = {queues:[]};

        $.get(SERVER_URL + "/api/wwf/queues")
            .then((data: IQueue[]) => {
                this.setState({ queues: data });
            }).fail((e) => alert(e));
    }


    render() {

        var body = this.state.queues.map(q =>
            <div key={q.name}>
                <h3>{q.name}</h3>
                <ul>
                    {q.tasks.map(t => <li> <Link to={`/RequestCard/${t.id}`}>{t.caption} №{t.number}</Link></li>)}
                </ul>
             </div>
        );

        return (<div>
                <Link  className="btn btn-blue" to="/RequestCard">Создать Заявку на выпуск карты</Link>
                {body}   
            </div>
            );
    }
}