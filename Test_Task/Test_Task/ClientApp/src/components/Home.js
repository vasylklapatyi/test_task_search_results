import React, { Component } from 'react';
import TextField from '@material-ui/core/TextField';
import FormControl from '@material-ui/core/FormControl';
import Select from '@material-ui/core/Select';
import Button from '@material-ui/core/Button';
import { DataGrid } from '@material-ui/data-grid';
import axios from 'axios';
const headCells = [
    {
        field: 'id',
        id: 'id',
        numeric: false,
        disablePadding: false,
        headerName: 'Id',
        width: 250,
        resizable: true
    },
    {
        field: 'timeStamp',
        id: 'timeStamp',
        numeric: false,
        disablePadding: true,
        headerName: 'TimeStamp',
        width: 150,
        resizable: true
    },
    {
        field: 'searchEngine',
        id: 'searchEngine',
        numeric: false,
        disablePadding: false,
        headerName: 'searchEngine',
        width: 100,
        resizable: true
    },
    {
        field: 'url',
        id: 'url',
        numeric: false,
        disablePadding: false,
        headerName: 'Url',
        width: 100,
        resizable: true
    },
    {
        field: 'title',
        id: 'title',
        numeric: true,
        disablePadding: false,
        headerName: 'title',
        width: 100,
        resizable: true
    }
];
var loaded = false;
export class Home extends Component {
    constructor(props) {
        super(props);
        this.handleClick = this.handleClick.bind(this);
        this.state = { data: [] };
        if (!loaded) {
            //the this variable in axios method references the inner method data,not the compontnt's 'this' variable
            var that = this;
            axios.get(window.location.origin + '/api/Task/GetGridData').then(function (response) {
                that.setState({ data: response.data });
            });
            loaded = true;
        }
    }
    componentWillUnmount() {
        loaded = false;
    }

    handleClick() {
        var searchingText = document.getElementById('textfield').value;
        axios.post(window.location.origin + '/api/Task/GetFromGoogle', searchingText).then(function (response) {
            //reload page functionality
            //here ill call GetGridData and reload the page with new data
        });

	}

  render () {
    return (
        <div>
            <div>
                <TextField label="Search term" variant="outlined" id="textfield"/>
                <FormControl variant="outlined" 
                    style={{ margin: "0" }}>
                    <Select native label="Search Engine" inputProps={{ name: 'searxchEngine', id: 'searchEngineSelect', }} >
                            <option value={"Google"} key={1}>Google</option>
                    </Select>
                </FormControl>
                <Button variant="contained" onClick={this.handleClick}>Search</Button>
            </div>
            <div style={{ height: 500 }}>
                <DataGrid rows={this.state.data} columns={headCells} pageSize={this.state.data.length} checkboxSelectiondisableColumnResize={false}  />
            </div>
      </div>
    );
  }
}
