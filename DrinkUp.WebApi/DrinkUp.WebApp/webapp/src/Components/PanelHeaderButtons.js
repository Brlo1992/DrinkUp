import React from 'react';
import { Row, Col, Button, Glyphicon, ProgressBar } from 'react-bootstrap';
import axios from 'axios';

export default class PanelHeaderButtons extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            id: this.props.drink.id,
            name: this.props.drink.name,
            like: this.props.drink.like,
            unlike: this.props.drink.unlike,
        }
    };

    Like = () => {
        axios.post(process.env.REACT_APP_DRINK_API_LOCAL + "api/rate/like", { Id: this.state.id })
            .then(this.setState((prevState, props) => ({
                like: prevState.like + 1
            })));
    };

    Unlike = () => {
        axios.post(process.env.REACT_APP_DRINK_API_LOCAL + "api/rate/unlike", { Id: this.state.id })
            .then(this.setState((prevState, props) => ({
                unlike: prevState.unlike + 1
            })));
    };

    render() {
        return <Row>
            <Col lg={5}>
                <h5>{this.state.name}</h5>
            </Col>
            <Col lg={7}>
                <Col lg={3}>
                    <Button bsStyle="warning" block onClick={this.Like}>
                        Edytuj <Glyphicon glyph="edit" />
                    </Button>
                </Col>
                <Col lg={3}>
                    <Button bsStyle="danger" block onClick={this.Like}>
                        Usuń <Glyphicon glyph="remove" />
                    </Button>
                </Col>
                <Col lg={2}>
                    <Button bsStyle="success" block onClick={this.Like}>
                        <Glyphicon glyph="heart-empty" />{this.state.like}
                    </Button>
                </Col>
                <Col lg={2}>
                    <Button bsStyle="danger" block onClick={this.Unlike}>
                        <Glyphicon glyph="flash" />{this.state.unlike}
                    </Button>
                </Col>
                <Col lg={2}>
                    <ProgressBar style={{ marginTop: "5px", marginBottom: "5px" }}>
                        <ProgressBar max={this.state.unlike + this.state.like}
                            now={this.state.like}
                            striped bsStyle="success" now={this.state.like} key={1} />
                        <ProgressBar max={this.state.unlike + this.state.like}
                            now={this.state.unlike}
                            striped bsStyle="danger" now={this.state.unlike} key={3} />
                    </ProgressBar>
                </Col>
            </Col>
        </Row>
    }
}