
import React, { Component } from "react";
//import {Map} from './Map';
import axios from 'axios'; // npm instal axios
import { forwardRef } from 'react';
import MaterialTable from "material-table";
import EditIcon from '@material-ui/icons/Edit';
import DeleteIcon from '@material-ui/icons/Delete';
import SaveIcon from '@material-ui/icons/Save';
import ArrowDownward from '@material-ui/icons/ArrowDownward';
import ChevronLeft from '@material-ui/icons/ChevronLeft';
import ChevronRight from '@material-ui/icons/ChevronRight';
import Clear from '@material-ui/icons/Clear';
import FilterList from '@material-ui/icons/FilterList';
import FirstPage from '@material-ui/icons/FirstPage';
import LastPage from '@material-ui/icons/LastPage';
import Search from '@material-ui/icons/Search';
// Import the detail table if you want use the collapsible section


const tableIcons = {
    Edit: forwardRef((props, ref) => <EditIcon {...props} ref={ref} />),
    Delete: forwardRef((props, ref) => <DeleteIcon {...props} ref={ref} />),
    Check: forwardRef((props, ref) => <SaveIcon {...props} ref={ref} />),
   Clear: forwardRef((props, ref) => <Clear {...props} ref={ref} />),
    DetailPanel: forwardRef((props, ref) => <ChevronRight {...props} ref={ref} />),
    Filter: forwardRef((props, ref) => <FilterList {...props} ref={ref} />),
    FirstPage: forwardRef((props, ref) => <FirstPage {...props} ref={ref} />),
    LastPage: forwardRef((props, ref) => <LastPage {...props} ref={ref} />),
    NextPage: forwardRef((props, ref) => <ChevronRight {...props} ref={ref} />),
    PreviousPage: forwardRef((props, ref) => <ChevronLeft {...props} ref={ref} />),
    ResetSearch: forwardRef((props, ref) => <Clear {...props} ref={ref} />),
    Search: forwardRef((props, ref) => <Search {...props} ref={ref} />),
    SortArrow: forwardRef((props, ref) => <ArrowDownward {...props} ref={ref} />),
  };

export default class Table extends Component {
    constructor(props) {
      super(props);
      this.state = {person: []};   
    }
      


    componentDidMount(prevProps) {    
        const maxResults = 20;    
        const url = `localhost:7103/GetPermissions`;
        axios.get(url)
        .then(results => {
          console.log(results);
          console.log(results.data.results);
          this.setState({ person: results.data.results });
    
          var newArr = results.data.results.map(function(val) {          
            return {
              id: val.id.value,
              NombreEmpleado: val.NombreEmpleado,
              ApellidoEmpleado: val.login.ApellidoEmpleado,
              FechaPermiso: val.FechaPermiso,          
              TipoPermiso: val.TipoPermiso.nombre
            };
          });
          console.log(results.data.results); 
          this.setState({
            tableArray: newArr  //set state of the weather5days
          },()=> {
             console.log(this.state.tableArray); 
             console.log('this.tableArray ',this.state.tableArray);
          });      
        });
      }


      render() {
        return (      
          <div style={{ maxWidth: "50%", marginLeft: "300px", marginTop: "100px" }}>
            <Table
              icons={tableIcons}
              options={{
                grouping: true
              }}
          
              columns={[
           ,
                { title: "Name", field: "name", align: 'left' },            
                { title: "Nombre", field: "NombreEmpleado"},            
                { title: "Apellido", field: "ApellidoEmpleado" },
                { title: "Fecha", field: "FechaPermiso" }      ,                                             
                { title: "Permiso", field: "TipoPermiso" }                                     
 
            ]}
              data={this.state.tableArray}      
            
              title="Permisos"
            />
          </div>
        );
      }
    }