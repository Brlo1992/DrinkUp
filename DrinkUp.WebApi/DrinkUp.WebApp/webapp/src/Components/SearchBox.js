import React from 'react'
import axios from 'axios'
import { Row, Col, Button, FormControl, } from 'react-bootstrap'
const style = {
    marignTop: "5px",
    marginBottom: "5px"
};
export default class SearchBox extends React.Component {
    constructor(props) {
        super(props)
    };

    handleClick = (name) => {
        axios.get();
    };

    render() {
        return <Row>
            <Col lg={10} style={style}>
                <FormControl type="text"></FormControl>
            </Col>
            <Col lg={2}>
                <Button bsStyle="primary" block>Szukaj</Button>
            </Col>
        </Row>;
    };
}