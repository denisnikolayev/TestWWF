
import "babel-polyfill";

import * as React from "react"
import {render} from "react-dom";
import {Index} from "./pages/index";
import {RequestCard} from "./pages/requestCard";

import {Router, Route, IndexRoute, browserHistory} from 'react-router';

export class Layout extends React.Component<{children:any}, {}> {      
    render() {
        return <div className="sheet">{this.props.children}</div>;
    }
}


const appNode = document.getElementById("app");

render(
    <Router history={browserHistory}>
            <Route path="/" component={Layout}>
            <IndexRoute component={Index}/>
            <Route path="RequestCard" component={RequestCard} />
            </Route>
        </Router>
    , appNode);
