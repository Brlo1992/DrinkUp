import React from 'react';
import { Row, Col, Button, Glyphicon, FormGroup, ControlLabel, FormControl } from 'react-bootstrap'

export default class AddPanel extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            name: "",
            description: "",
            ingredients: [],
            glass: ""
        }
    }

    handleChange = (event) => {
        this.setState({ [event.target.name]: event.target.value });
    }

    createInput = (name, displayName, type, placeholder) => <FormGroup controlId={name}>
        <ControlLabel>{displayName}</ControlLabel>
        <FormControl
            type={type}
            name={name}
            value={this.state.name}
            placeholder={placeholder}
            onChange={this.handleChange}
        />
    </FormGroup>

    createTextArea = (name, displayName, placeholder) => <FormGroup controlId={name}>
        <ControlLabel>{displayName}</ControlLabel>
        <FormControl name={name} componentClass="textarea" placeholder={placeholder} />
    </FormGroup>

    createSelectBox = (name, displayName, placeholder, options) => <FormGroup controlId={name}>
        <ControlLabel>{displayName}</ControlLabel>
        <FormControl name={name} componentClass="select" placeholder={placeholder}>
            {options.map(option => <option value={option}>{option}</option>)}
        </FormControl>
    </FormGroup>

    createInputForList = () => {
        <Row>
            <FormGroup controlId={name}>
                <Col lg={10}><ControlLabel>{displayName}</ControlLabel>
                    <FormControl name={name} componentClass="select" placeholder={placeholder}>
                        {options.map(option => <option value={option}>{option}</option>)}
                    </FormControl>S</Col>
                <Col lg={2}>
                    <Button bsStyle="success" block onClick={this.props.setToDisplay}>
                        Anuluj <Glyphicon glyph="plus" />
                    </Button>
                </Col>
            </FormGroup>
        </Row>
    }

    render() {
        return <div>
            <Row>
                <Col lg={10}>
                    <h2>Dodaj nowy</h2>
                </Col>
                <Col lg={2}>
                    <Button bsStyle="waring" block onClick={this.props.setToDisplay}>
                        Anuluj <Glyphicon glyph="logout" />
                    </Button>
                </Col>
            </Row>
            <Row></Row>
            <form>
                {this.createInput("name", "Nazwa", "text", "Podaj nazwę")}
                {this.createTextArea("descrition", "Opis", "Podaj opis")}
                {this.createSelectBox("glass", "Szkło", "Wybierz typ szkła", ["highball", "tumbler"])}
            </form>
        </div>
    }
}