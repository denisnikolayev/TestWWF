import * as React from "react";

export interface ISearchModel {
    iin?: string;
    documentNumber?:string;
}

export class Search extends React.Component<{onChange:(model:ISearchModel)=>void}, ISearchModel> {
    constructor() {
        super();

        this.state = {  };
    }

    onChange(model: ISearchModel) {
        this.setState(model);
        this.props.onChange(this.state);
    }

    render() {
        return <div>
                    <h2>Поиск клиента</h2>
                    <div>ИИН: </div>
                    <div><input type="text" value={this.state.iin}
                        onChange={(e: any) => this.onChange({ iin: e.target.value }) } /></div>

                    <div>Номер документа: </div>
                    <div><input type="text" value={this.state.documentNumber}
                        onChange={(e: any) => this.onChange({ documentNumber: e.target.value }) } /> </div>
               </div>;

    }
}