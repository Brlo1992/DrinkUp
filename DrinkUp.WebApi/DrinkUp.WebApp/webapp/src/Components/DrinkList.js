import React from 'react';
import axios from 'axios';
import { Panel, Accordion, Well, Row, Col } from 'react-bootstrap';

export default class DrinkList extends React.Component {
    constructor(props) {
        super();
        this.state = {
            drinks: [],
            contentLoaded: false
        };
    };

    componentDidMount() {
        axios.get("http://localhost:58785/api/drink")
            .then(response => this.setState({
                drinks: response.data.data,
                contentLoaded: true
            }));
    };

    render() {
        let component = <div></div>
        if (this.state.contentLoaded === true) {
            component = <div>
                {this.state.drinks.map((drink, index) =>
                    <Panel key={index} header={drink.name} bsStyle="primary">
                        <Row>
                            <Col lg={6}><h5>{drink.description}</h5></Col>
                            <Col lg={6}>
                                <Accordion>
                                    <Panel header="Składniki" eventKey="1">
                                        {drink.ingredients.map((ingredient, index) =>
                                            <Well key={index}>{ingredient}</Well>)}
                                    </Panel>
                                </Accordion>
                            </Col>
                        </Row>
                    </Panel>)}
            </div>
        }
        return component;
    }
};