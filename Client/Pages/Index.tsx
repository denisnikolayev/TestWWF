import * as React from "react"
import { Router, Route, Link } from 'react-router'

export class Index extends React.Component<{}, {}> {
    //TODO: отобразить очереди

    render() {
        return (<div>
                <h2>Менеджер</h2>
                <Link  className="btn btn-blue" to="/RequestCard">Создать Заявку на выпуск карты</Link>
            </div>
            );
    }
}