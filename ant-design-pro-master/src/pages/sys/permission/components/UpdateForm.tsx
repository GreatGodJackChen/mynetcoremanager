import { FormComponentProps } from 'antd/es/form';
import { TableListItem } from '../data.d';
import React, { Component } from 'react';
import { Button, DatePicker, Form, Input, Modal, Radio, Select } from 'antd';
import TreeBind from './Tree/index';

const FormItem = Form.Item;


export interface FormValsType extends Partial<TableListItem> {
}

export interface UpdateFormState {
  formVals: FormValsType;
}
export interface UpdateFormProps extends FormComponentProps {
  handleUpdateModalVisible: (flag?: boolean, formVals?: FormValsType) => void;
  values: Partial<TableListItem>;
  handleUpdate: (values: FormValsType) => void;
  updateModalVisible: boolean;
}
class UpdateForm extends Component<UpdateFormProps, UpdateFormState> {
  static defaultProps = {
    handleUpdate: () => { },
    handleUpdateModalVisible: () => { },
    values: {},
  };
  constructor(props: UpdateFormProps) {
    super(props);

    this.state = {
      formVals: {
        name: props.values.name,
        actionCode: props.values.actionCode,
        id: props.values.id,
        menuId: props.values.menuId,
      },
    };
  }
  okHandle = () => {
    const { form, handleUpdate } = this.props;
    const { validateFields } = this.props.form;
    const { formVals: oldValue } = this.state;
    validateFields(['name', 'actionCode','menuId'],(err, fieldsValue) => {
      if (err) return;
      form.resetFields();
      const formVals = { ...oldValue, ...fieldsValue };
      this.setState(
        {
          formVals,
        },
        () => {
          handleUpdate(formVals);
        }
      );
    });
  };
  render() {
    const { updateModalVisible, handleUpdateModalVisible, form } = this.props;
    return (
      <Modal
        key={this.state.formVals}
        destroyOnClose
        title="编辑"
        visible={updateModalVisible}
        onOk={this.okHandle}
        onCancel={() => handleUpdateModalVisible()}
      >
        <FormItem labelCol={{ span: 5 }} wrapperCol={{ span: 15 }} label="名称">
          {form.getFieldDecorator('name', {
            rules: [{ required: true, message: '请输入至少2个字符的规则描述！', min: 2 }],
            initialValue: this.state.formVals.name,
          })(<Input placeholder="请输入" />)}
        </FormItem>
        <FormItem labelCol={{ span: 5 }} wrapperCol={{ span: 15 }} label="操作码">
          {form.getFieldDecorator('actionCode', {
            rules: [{ required: true, message: '请输入至少2个字符的规则描述！', min: 2 }],
            initialValue: this.state.formVals.actionCode,
          })(<Input placeholder="请输入" />)}
        </FormItem>
        <FormItem labelCol={{ span: 5 }} wrapperCol={{ span: 15 }} label="关联菜单">
          {form.getFieldDecorator('menuId', {
            rules: [{ required: true, }],
            initialValue: this.state.formVals.menuId,
          })(<TreeBind />)}
        </FormItem>
      </Modal>
    );
  }
}
export default Form.create<UpdateFormProps>()(UpdateForm);
