import React from 'react'
import axios from 'axios'
import { Row, Col, Button, FormControl, Glyphicon} from 'react-bootstrap'
const style = {
    marignTop: "5px",
    marginBottom: "5px"
};
export default class SearchBox extends React.Component {
    constructor(props) {
        super(props)
        this.state = {
            searchName: ""
        };
        this.handleChange = this.handleChange.bind(this);
    };

    handleChange(event) {
        this.setState({ searchName: event.target.value });
        this.props.selectDrinks(event.target.value);
    };

    render() {
        return <Row>
            <Col lg={10} style={style}>
                <FormControl type="text" value={this.state.searchName} onChange={this.handleChange}></FormControl>
            </Col>
            <Col lg={2} style={style}>
                <Button bsStyle="primary" block onClick={this.props.setToAdd}>
                    Nowy <Glyphicon glyph="plus" />
                </Button>
            </Col>
        </Row>;
    };
}