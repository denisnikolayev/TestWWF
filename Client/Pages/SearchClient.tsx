import * as React from "react"
import {Link } from 'react-router'

export class SearchClient extends React.Component<{}, { }> {

    choiceCustomer() {
        return <div className="col-md-12 form-group">
            <div className="row form-group" style={{ margin: "10px 0 0 0" }}>
                    <button type="button" className="btn btn-default col-md-3">Считать данные с ЧИПа</button>
                    <div className="checkbox col-md-5" style={{ textAlign:"right"}}>
                    <label>
                        <input type="checkbox"/> Поиск по данным с ЧИПа
                    </label>
                </div>
            </div>

            <div className="row form-group">
                <label className="col-md-1">ИИН</label>
                <input type="text" className=" form-control col-md-8" maxLength={12} style={{ width: "200px" }} placeholder="12 знаков"/>
            </div>

            <div className="row form-group">
                <label className="col-md-12">Дополнительные параметры</label>
            </div>

            <div className="row form-group">
                <label className="col-md-3">Фамилия</label>
                <input type="text" className=" form-control col-md-8" maxLength={12} style={{ width: "500px" }} placeholder="Фамилия"/>
            </div>
            <div className="row form-group">
                <label className="col-md-3">Имя</label>
                <input type="text" className=" form-control col-md-8" maxLength={12} style={{ width: "500px" }} placeholder="Имя"/>
            </div>
            <div className="row form-group">
                <label className="col-md-3">Отчество</label>
                <input type="text" className=" form-control col-md-8" maxLength={12} style={{ width: "500px" }} placeholder="Отчество"/>
            </div>
            <div className="row form-group">
                <label className="col-md-3">Дата рождения</label>
                <div className="input-group date" data-provide="datepicker">
                    <input type="date" className="form-control" style={{ width: "500px" }} maxLength={12}/>
            </div>
            </div>

        </div>;
    };



    render() {
        return (<div className="">
                <div className="col-md-12 border"> Создание заявки </div>
            <div className="body-content" >
                <div className="col-md-2"> Этапы создания заявки </div>
                <div className="col-md-10 container" style={{ borderLeft: "1px solid black" }}> {this.choiceCustomer() } </div>
            </div>  
            </div>);
    }
}