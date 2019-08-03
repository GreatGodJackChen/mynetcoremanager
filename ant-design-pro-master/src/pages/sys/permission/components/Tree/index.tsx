import { TreeSelect } from 'antd';
const { TreeNode } = TreeSelect;
import React from 'react';

import { mygetAuthority } from '../../../../../utils/authority';
const treeData = mygetAuthority('menu');

interface TreeProps{
  onChange?: (key: string) => void;
  value: string;
}

class TreeBind extends React.Component<TreeProps>{
  constructor(props) {
    super(props);
    this.state = {
      value: props.value,
    }
  }
  handleChange = (val: string) => {
    this.setState({ value: val });
    this.props.onChange(val);

  }
  //onSwitch = (value: string) => {
  //  this.setState(
  //    {
  //      value,
  //    },
  //    () => {
  //      const { onTreeChange } = this.props;
  //      if (onTreeChange) {
  //        onTreeChange(value);
  //      }
  //    },
  //  );
  //};
  onChange = (value: string) => {
    this.setState({ value }, () => { alert(this.state.value); });
  };

  renderTreeNodes = data =>
    data.map(item => {
      if (item.children) {
        return (
          <TreeNode title={item.name} key={item.id} value={item.id} dataRef={item}>
            {this.renderTreeNodes(item.children)}
          </TreeNode>
        );
      }
      return <TreeNode {...item} />;
    });

  render(){
    return (
      <TreeSelect
        style={{ width: 300 }}
        value={this.state.value}
        dropdownStyle={{ maxHeight: 400, overflow: 'auto' }}
        placeholder="请选择"
        onChange={this.handleChange}
        allowClear
        defaultValue={this.state.value}
      >
        {this.renderTreeNodes(treeData)}
      </TreeSelect>
    );
  }
};

export default TreeBind;
