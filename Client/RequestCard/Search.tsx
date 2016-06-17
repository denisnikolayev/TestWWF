import * as React from "react";

export interface ISearchModel {
    iin?: string;
    documentNumber?:string;
}

export class Search extends React.Component<{onChange:(model:ISearchModel)=>void, model:{message?:string}}, ISearchModel> {
    constructor() {
        super();

        this.state = {  };
    }

    onChange(model: ISearchModel) {
        this.setState(model, () => this.props.onChange(this.state));
    }

    render() {
        var message = null;
        if (this.props.model.message) {
            message = <p className="bg-danger">{this.props.model.message}</p>;
        }

        return <form>
            <h2 style={{ borderBottom: "1px solid black" }}>Поиск клиента</h2>
                    <div className="form-group">
                        <label>ИИН: </label>
                        <input style={{ width: "200px" }} maxLength={12} className="form-control" type="text" value={this.state.iin}
                            onChange={(e: any) => this.onChange({ iin: e.target.value }) } />
                    </div>
                    <div className="form-group">
                        <label>Номер документа: </label>
                        <input style={{ width: "300px" }} className="form-control" type="text" value={this.state.documentNumber}
                            onChange={(e: any) => this.onChange({ documentNumber: e.target.value }) } />
                    </div>

                    {message}
               </form>;

    }
}