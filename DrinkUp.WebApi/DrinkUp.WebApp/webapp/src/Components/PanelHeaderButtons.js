import React from 'react';
import { Row, Col, Button, Glyphicon } from 'react-bootstrap';
import axios from 'axios';

export default class PanelHeaderButtons extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            name: props.drink.name,
            like: props.drink.like,
            unlike: props.drink.unlike,
        }
    };

    Like = () => {
        axios.put()
            .then(this.setState((prevState, props) => ({
                like: prevState.like + 1
            })));
    };

    Unlike = () => {
        axios.put()
            .then(this.setState((prevState, props) => ({
                unlike: prevState.unlike + 1
            })));
    };

    render() {
        return <Row>
            <Col lg={8}>
                <h5>{this.state.name}</h5>
            </Col>
            <Col lg={2}>
                <Button bsStyle="success" block onClick={this.Like}>
                    Like <Glyphicon glyph="heart-empty" /> {this.state.like}
                </Button>
            </Col>
            <Col lg={2}>
                <Button bsStyle="danger" block onClick={this.Unlike}>
                    Unlike <Glyphicon glyph="flash" />{this.state.unlike}
                </Button>
            </Col>
        </Row>
    }
}