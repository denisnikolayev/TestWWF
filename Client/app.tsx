
import "babel-polyfill";
import "bootstrap";
import "bootstrap/dist/css/bootstrap"

import * as React from "react"
import {render} from "react-dom";
import {SearchClient} from "./pages/SearchClient";
import {Header} from "./requestDebitCard/Header";

import {Router, Route, IndexRoute, browserHistory} from 'react-router';

export class Layout extends React.Component<{children:any}, {}> {      
    render() {
        return <div className="container" style={{ backgroundColor: "white", marginTop: "10px"}}>
            <div className="page-header row">
                <div className="col-md-10"> Заявка на выпуск дебитовой карточки</div>
                <img className="img-rounded col-md-2" src={"./images/LogoHalykBank.jpg"} alt="Украли картинку"/>
            </div>
            <div className="body-content">
                <div className="">{this.props.children}</div>
            </div>
        </div>;
    }
}

const appNode = document.getElementById("app");

render(
    
    <Router history={browserHistory}>
        <Route path="/" component={Layout}>
            <IndexRoute component={SearchClient} /> 
            </Route>
        </Router>
    , appNode);
