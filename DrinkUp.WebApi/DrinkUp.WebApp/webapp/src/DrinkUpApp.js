import React, { Component } from 'react';
import SearchBox from './Components/SearchBox.js';
import DrinkList from './Components/DrinkList.js';
import axios from 'axios'

const style = {
    margin: "10px",
    padding: "10px"
};

const headerStyle = {
    textAlign: "center",
    textTransform: "uppercase",
    padding: "20px"
};

const headerContainerStyle = {
    backgroundColor: "#337ab7",
    borderRadius: "10px",
    color: "white"
};

const containerStyle = {
    borderRadius: "10px",
    border: "2px solid #337ab7",
    padding: "20px"
};

export default class DrinkUpApp extends Component {
    constructor(props) {
        super(props);
    };

    handleClick = (name) => {
        // axios.get("http://localhost:58785/api/drink")
        //     .then(response => this.setState({
        //         drinks: response.Data
        //     }));
    };

    render() {
        return <div style={style}>
            <div style={headerContainerStyle} >
                <h1 style={headerStyle}>Drink Up React Client</h1>
            </div>
            <div style={containerStyle}>
                <SearchBox onClick={this.handleClick} />
                <br />
                <DrinkList/>
            </div>
        </div >;
    };
}

