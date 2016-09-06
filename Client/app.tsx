﻿
import "babel-polyfill";
import "bootstrap";
import "bootstrap/dist/css/bootstrap"

import * as React from "react"
import {render} from "react-dom";
import {Index} from "./pages/index";
import {RequestCardPage} from "./RequestCard/requestCardPage";

import {Router, Route, IndexRoute, browserHistory} from 'react-router';

import {Test} from "../RequestCardClient/test.ts";

export class Layout extends React.Component<{children:any}, {}> {      
    render() {
        return <div className="sheet">{this.props.children}</div>;
    }
}

alert(Test());

const appNode = document.getElementById("app");

render(
    <Router history={browserHistory}>
            <Route path="/" component={Layout}>
            <IndexRoute component={Index}/>
            <Route path="RequestCard/:id" component={RequestCardPage} />
            <Route path="RequestCard" component={RequestCardPage} />
            </Route>
        </Router>
    , appNode);
