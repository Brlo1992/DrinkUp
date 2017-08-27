import React from 'react';
import { Panel, Accordion, Well, Row, Col } from 'react-bootstrap';
import PanelHeaderButtons from './PanelHeaderButtons.js'


export default class DrinkList extends React.Component {
    constructor(props) {
        super();
    };

    render() {
        let component = <div></div>
        if (this.props.loaded === true) {
            component = <div>
                {this.props.drinks.map((drink, index) =>
                    <Panel key={index} header={<PanelHeaderButtons name={drink.name} />} bsStyle="primary">
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