import React, { Component } from 'react';
import SearchBox from './Components/SearchBox.js';
import DrinkList from './Components/DrinkList.js';
import AddPanel from './Components/AddPanel.js';
import axios from 'axios';

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

const mode = {
    ADD: "Add",
    DISPLAY: "display"
};

export default class DrinkUpApp extends Component {
    constructor(props) {
        super(props);
        this.state = {
            drinks: [],
            contentLoaded: false,
            searchedDrinks: [],
            somethingChanged: true,
            mode: mode.DISPLAY
        };
        this.selectDrinks = this.selectDrinks.bind(this);
        this.loadData = this.loadData.bind(this);
        this.setToDisplay = this.setToDisplay.bind(this);
        this.setToAdd = this.setToAdd.bind(this);
    };

    componentDidMount() {
        this.loadData();
    };

    setToDisplay() {
        this.setState({
            mode: mode.DISPLAY
        });
    };

    setToAdd() {
        this.setState({
            mode: mode.ADD
        });
    };

    loadData() {
        axios.get(process.env.REACT_APP_DRINK_API_LOCAL + "api/drink")
            .then(response => this.setState({
                drinks: response.data.data,
                contentLoaded: true,
                searchedDrinks: response.data.data
            }));
    };

    selectDrinks(value) {
        this.setState({
            searchedDrinks: this.state.drinks.filter(drink => drink.name.includes(value))
        });
    };

    render() {
        let element = <div>
            <SearchBox selectDrinks={this.selectDrinks} setToAdd={this.setToAdd} />
            <br />
            <DrinkList drinks={this.state.searchedDrinks}
                loaded={this.state.contentLoaded} />
        </div>;
        if (this.state.mode === mode.ADD)
            element = <AddPanel setToDisplay={this.setToDisplay} />;
        return <div style={style}>
            <div style={headerContainerStyle} >
                <h1 style={headerStyle}>Drink Up React Client</h1>
            </div>
            <div style={containerStyle}>
                {element}
            </div>
        </div >;
    };
}

