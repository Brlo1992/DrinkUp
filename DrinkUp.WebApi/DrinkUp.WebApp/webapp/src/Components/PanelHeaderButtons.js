import React from 'react';
import { Row, Col, Button } from 'react-bootstrap';
import axios from 'axios';

const Like = (name) => {
    
};

const Unlike = (name) => {
    
};

const PanelHeaderButtons = (props) => <Row>
    <Col lg={8}>
        <h5>{props.name}</h5>
    </Col>
    <Col lg={2}>
        <Button bsStyle="success" block>Like</Button>
    </Col>
    <Col lg={2}>
        <Button bsStyle="danger" block>Unlike</Button>
    </Col>
</Row>

export default PanelHeaderButtons;