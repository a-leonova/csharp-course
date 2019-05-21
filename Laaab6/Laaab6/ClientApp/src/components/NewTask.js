import React, { Component } from 'react';
import './NewTask.css';
import {Glyphicon} from 'react-bootstrap';


export class NewTask extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            taskName: '',
            backpackWeight: '',
            rows: [{ name: '', weight: 0, cost: 0 }]
        };
    }

    onSubmit = (event) => {
        event.preventDefault();
        console.log("Submit!");

        const { taskName, backpackWeight, rows } = this.state;
        var data = { "taskName": taskName, "backpackWeight": backpackWeight, "things": rows }; 

        fetch('api/task/ololopost',
            {
                method: 'POST', // *GET, POST, PUT, DELETE, etc.
                headers: {
                    'Accept': 'application/json',
                    'Content-Type': 'application/json'
                    //'Content-Type': 'application/x-www-form-urlencoded'
                },
                body: JSON.stringify(data) // body data type must match "Content-Type" header
            });
    
    }

    handleChange = (event) => {
        console.log("4");
        const target = event.target;
        const value = target.value;
        const name = target.name;
        const id = Number(target.id);

        if (name === "name") {
            this.setState({
                rows: this.state.rows.map((item, index) => {
                    if (index !== id) return item;
                    return {
                        ...item,
                        name: value
                    }
                })
            });
        }
        if (name === "weight") {
            this.setState({
                rows: this.state.rows.map((item, index) => {
                    if (index !== id) return item;
                    return {
                        ...item,
                        weight: value
                    }
                })
            });
        }
        if (name === "cost") {
            this.setState({
                rows: this.state.rows.map((item, index) => {
                    if (index !== id) return item;
                    return {
                        ...item,
                        cost: Number(value)
                    }
                })
            });
        }
        else {
            this.setState({
                [name]: value
            });
        }
    }

    CreateRow = (e) => {
        console.log("1");
        e.preventDefault();
        this.setState({
            rows: this.state.rows.concat({ name: '', weight: 0, cost: 0 })
        });
        console.log("2");
    }

    RemoveRow = (e) => {
        e.preventDefault();
        const { rows } = this.state;
        
       const id = e.currentTarget.dataset.id;
       //var id = Number(e.target.id);
        console.log(id);
        rows.splice(id, 1);

        this.setState({
            rows: rows
        });
    }

    render() {
        console.log("render");
        const { rows } = this.state;
        return (
            <form onSubmit={this.onSubmit}>
                <input className="rowStyle" name="taskName" type="text" placeholder="Task name" onChange={this.handleChange}/>
                <input className="rowStyle" name="backpackWeight" type="number" min="0" placeholder="Backpack capacity" onChange={this.handleChange}/>

                <table className="myTable">
                    <thead>
                        <tr>
                            <td>Name</td>
                            <td>Cost</td>
                            <td>Weight</td>
                            <td></td>
                        </tr>
                    </thead>

                    <tbody>
                        {
                            rows.map((item, index) => (
                                <tr key={index}>
                            <td>
                                <input id={index} type="text" name="name" placeholder="Name" value={item.name} form="my_form" onChange={this.handleChange}/>
                            </td>
                            <td>
                                <input id={index} type="number" name="cost" min="0" placeholder="Cost" value={item.cost} form="my_form" onChange={this.handleChange} />
                            </td>
                            <td>
                                <input id={index} type="number" name="weight" min="0" placeholder="Weight" value={item.weight} form="my_form" onChange={this.handleChange} />
                            </td>
                            <td>
                                        <button data-id={index} onClick={this.RemoveRow}><span className="glyphicon glyphicon-trash"></span></button>
                            </td>

                        </tr>
                        ))
                }
                       
                    </tbody>
                </table>

                <button onClick={this.CreateRow} >Create row</button>

                <input className="applyButton" type="submit" value="Apply" />
            </form>
        );
    }
}