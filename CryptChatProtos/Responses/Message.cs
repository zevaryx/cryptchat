// <auto-generated>
//     Generated by the protocol buffer compiler.  DO NOT EDIT!
//     source: message.proto
// </auto-generated>
#pragma warning disable 1591, 0612, 3021
#region Designer generated code

using pb = global::Google.Protobuf;
using pbc = global::Google.Protobuf.Collections;
using pbr = global::Google.Protobuf.Reflection;
using scg = global::System.Collections.Generic;
namespace CryptChatProtos.Responses.Message {

  /// <summary>Holder for reflection information generated from message.proto</summary>
  public static partial class MessageReflection {

    #region Descriptor
    /// <summary>File descriptor for message.proto</summary>
    public static pbr::FileDescriptor Descriptor {
      get { return descriptor; }
    }
    private static pbr::FileDescriptor descriptor;

    static MessageReflection() {
      byte[] descriptorData = global::System.Convert.FromBase64String(
          string.Concat(
            "Cg1tZXNzYWdlLnByb3RvEiFjcnlwdGNoYXRwcm90b3MucmVzcG9uc2VzLm1l",
            "c3NhZ2UisQEKD01lc3NhZ2VSZXNwb25zZRILCgNfaWQYASABKAkSDAoEY2hh",
            "dBgCIAEoCRIPCgdtZXNzYWdlGAMgASgJEgsKA2tleRgEIAEoCRIRCglzaWdu",
            "YXR1cmUYBSABKAkSEQoJdGltZXN0YW1wGAYgASgBEg4KBnNlbmRlchgHIAEo",
            "CRIOCgZlZGl0ZWQYCCABKAgSEQoJZWRpdGVkX2F0GAkgASgBEgwKBGZpbGUY",
            "CiABKAkiWwoTTWVzc2FnZUxpc3RSZXNwb25zZRJECghtZXNzYWdlcxgBIAMo",
            "CzIyLmNyeXB0Y2hhdHByb3Rvcy5yZXNwb25zZXMubWVzc2FnZS5NZXNzYWdl",
            "UmVzcG9uc2UiQwoTU2VuZE1lc3NhZ2VSZXNwb25zZRILCgNfaWQYASABKAkS",
            "DAoEY2hhdBgCIAEoCRIRCgl0aW1lc3RhbXAYAyABKAEiRQoTRWRpdE1lc3Nh",
            "Z2VSZXNwb25zZRIOCgZlZGl0ZWQYASABKAgSEQoJZWRpdGVkX2F0GAIgASgB",
            "EgsKA19pZBgDIAEoCSInChVEZWxldGVNZXNzYWdlUmVzcG9uc2USDgoGc3Rh",
            "dHVzGAEgASgJQiSqAiFDcnlwdENoYXRQcm90b3MuUmVzcG9uc2VzLk1lc3Nh",
            "Z2ViBnByb3RvMw=="));
      descriptor = pbr::FileDescriptor.FromGeneratedCode(descriptorData,
          new pbr::FileDescriptor[] { },
          new pbr::GeneratedClrTypeInfo(null, new pbr::GeneratedClrTypeInfo[] {
            new pbr::GeneratedClrTypeInfo(typeof(global::CryptChatProtos.Responses.Message.MessageResponse), global::CryptChatProtos.Responses.Message.MessageResponse.Parser, new[]{ "Id", "Chat", "Message", "Key", "Signature", "Timestamp", "Sender", "Edited", "EditedAt", "File" }, null, null, null),
            new pbr::GeneratedClrTypeInfo(typeof(global::CryptChatProtos.Responses.Message.MessageListResponse), global::CryptChatProtos.Responses.Message.MessageListResponse.Parser, new[]{ "Messages" }, null, null, null),
            new pbr::GeneratedClrTypeInfo(typeof(global::CryptChatProtos.Responses.Message.SendMessageResponse), global::CryptChatProtos.Responses.Message.SendMessageResponse.Parser, new[]{ "Id", "Chat", "Timestamp" }, null, null, null),
            new pbr::GeneratedClrTypeInfo(typeof(global::CryptChatProtos.Responses.Message.EditMessageResponse), global::CryptChatProtos.Responses.Message.EditMessageResponse.Parser, new[]{ "Edited", "EditedAt", "Id" }, null, null, null),
            new pbr::GeneratedClrTypeInfo(typeof(global::CryptChatProtos.Responses.Message.DeleteMessageResponse), global::CryptChatProtos.Responses.Message.DeleteMessageResponse.Parser, new[]{ "Status" }, null, null, null)
          }));
    }
    #endregion

  }
  #region Messages
  public sealed partial class MessageResponse : pb::IMessage<MessageResponse> {
    private static readonly pb::MessageParser<MessageResponse> _parser = new pb::MessageParser<MessageResponse>(() => new MessageResponse());
    private pb::UnknownFieldSet _unknownFields;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pb::MessageParser<MessageResponse> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::CryptChatProtos.Responses.Message.MessageReflection.Descriptor.MessageTypes[0]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public MessageResponse() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public MessageResponse(MessageResponse other) : this() {
      Id_ = other.Id_;
      chat_ = other.chat_;
      message_ = other.message_;
      key_ = other.key_;
      signature_ = other.signature_;
      timestamp_ = other.timestamp_;
      sender_ = other.sender_;
      edited_ = other.edited_;
      editedAt_ = other.editedAt_;
      file_ = other.file_;
      _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public MessageResponse Clone() {
      return new MessageResponse(this);
    }

    /// <summary>Field number for the "_id" field.</summary>
    public const int IdFieldNumber = 1;
    private string Id_ = "";
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public string Id {
      get { return Id_; }
      set {
        Id_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    /// <summary>Field number for the "chat" field.</summary>
    public const int ChatFieldNumber = 2;
    private string chat_ = "";
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public string Chat {
      get { return chat_; }
      set {
        chat_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    /// <summary>Field number for the "message" field.</summary>
    public const int MessageFieldNumber = 3;
    private string message_ = "";
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public string Message {
      get { return message_; }
      set {
        message_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    /// <summary>Field number for the "key" field.</summary>
    public const int KeyFieldNumber = 4;
    private string key_ = "";
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public string Key {
      get { return key_; }
      set {
        key_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    /// <summary>Field number for the "signature" field.</summary>
    public const int SignatureFieldNumber = 5;
    private string signature_ = "";
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public string Signature {
      get { return signature_; }
      set {
        signature_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    /// <summary>Field number for the "timestamp" field.</summary>
    public const int TimestampFieldNumber = 6;
    private double timestamp_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public double Timestamp {
      get { return timestamp_; }
      set {
        timestamp_ = value;
      }
    }

    /// <summary>Field number for the "sender" field.</summary>
    public const int SenderFieldNumber = 7;
    private string sender_ = "";
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public string Sender {
      get { return sender_; }
      set {
        sender_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    /// <summary>Field number for the "edited" field.</summary>
    public const int EditedFieldNumber = 8;
    private bool edited_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool Edited {
      get { return edited_; }
      set {
        edited_ = value;
      }
    }

    /// <summary>Field number for the "edited_at" field.</summary>
    public const int EditedAtFieldNumber = 9;
    private double editedAt_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public double EditedAt {
      get { return editedAt_; }
      set {
        editedAt_ = value;
      }
    }

    /// <summary>Field number for the "file" field.</summary>
    public const int FileFieldNumber = 10;
    private string file_ = "";
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public string File {
      get { return file_; }
      set {
        file_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override bool Equals(object other) {
      return Equals(other as MessageResponse);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool Equals(MessageResponse other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (Id != other.Id) return false;
      if (Chat != other.Chat) return false;
      if (Message != other.Message) return false;
      if (Key != other.Key) return false;
      if (Signature != other.Signature) return false;
      if (!pbc::ProtobufEqualityComparers.BitwiseDoubleEqualityComparer.Equals(Timestamp, other.Timestamp)) return false;
      if (Sender != other.Sender) return false;
      if (Edited != other.Edited) return false;
      if (!pbc::ProtobufEqualityComparers.BitwiseDoubleEqualityComparer.Equals(EditedAt, other.EditedAt)) return false;
      if (File != other.File) return false;
      return Equals(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override int GetHashCode() {
      int hash = 1;
      if (Id.Length != 0) hash ^= Id.GetHashCode();
      if (Chat.Length != 0) hash ^= Chat.GetHashCode();
      if (Message.Length != 0) hash ^= Message.GetHashCode();
      if (Key.Length != 0) hash ^= Key.GetHashCode();
      if (Signature.Length != 0) hash ^= Signature.GetHashCode();
      if (Timestamp != 0D) hash ^= pbc::ProtobufEqualityComparers.BitwiseDoubleEqualityComparer.GetHashCode(Timestamp);
      if (Sender.Length != 0) hash ^= Sender.GetHashCode();
      if (Edited != false) hash ^= Edited.GetHashCode();
      if (EditedAt != 0D) hash ^= pbc::ProtobufEqualityComparers.BitwiseDoubleEqualityComparer.GetHashCode(EditedAt);
      if (File.Length != 0) hash ^= File.GetHashCode();
      if (_unknownFields != null) {
        hash ^= _unknownFields.GetHashCode();
      }
      return hash;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override string ToString() {
      return pb::JsonFormatter.ToDiagnosticString(this);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void WriteTo(pb::CodedOutputStream output) {
      if (Id.Length != 0) {
        output.WriteRawTag(10);
        output.WriteString(Id);
      }
      if (Chat.Length != 0) {
        output.WriteRawTag(18);
        output.WriteString(Chat);
      }
      if (Message.Length != 0) {
        output.WriteRawTag(26);
        output.WriteString(Message);
      }
      if (Key.Length != 0) {
        output.WriteRawTag(34);
        output.WriteString(Key);
      }
      if (Signature.Length != 0) {
        output.WriteRawTag(42);
        output.WriteString(Signature);
      }
      if (Timestamp != 0D) {
        output.WriteRawTag(49);
        output.WriteDouble(Timestamp);
      }
      if (Sender.Length != 0) {
        output.WriteRawTag(58);
        output.WriteString(Sender);
      }
      if (Edited != false) {
        output.WriteRawTag(64);
        output.WriteBool(Edited);
      }
      if (EditedAt != 0D) {
        output.WriteRawTag(73);
        output.WriteDouble(EditedAt);
      }
      if (File.Length != 0) {
        output.WriteRawTag(82);
        output.WriteString(File);
      }
      if (_unknownFields != null) {
        _unknownFields.WriteTo(output);
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int CalculateSize() {
      int size = 0;
      if (Id.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(Id);
      }
      if (Chat.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(Chat);
      }
      if (Message.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(Message);
      }
      if (Key.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(Key);
      }
      if (Signature.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(Signature);
      }
      if (Timestamp != 0D) {
        size += 1 + 8;
      }
      if (Sender.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(Sender);
      }
      if (Edited != false) {
        size += 1 + 1;
      }
      if (EditedAt != 0D) {
        size += 1 + 8;
      }
      if (File.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(File);
      }
      if (_unknownFields != null) {
        size += _unknownFields.CalculateSize();
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(MessageResponse other) {
      if (other == null) {
        return;
      }
      if (other.Id.Length != 0) {
        Id = other.Id;
      }
      if (other.Chat.Length != 0) {
        Chat = other.Chat;
      }
      if (other.Message.Length != 0) {
        Message = other.Message;
      }
      if (other.Key.Length != 0) {
        Key = other.Key;
      }
      if (other.Signature.Length != 0) {
        Signature = other.Signature;
      }
      if (other.Timestamp != 0D) {
        Timestamp = other.Timestamp;
      }
      if (other.Sender.Length != 0) {
        Sender = other.Sender;
      }
      if (other.Edited != false) {
        Edited = other.Edited;
      }
      if (other.EditedAt != 0D) {
        EditedAt = other.EditedAt;
      }
      if (other.File.Length != 0) {
        File = other.File;
      }
      _unknownFields = pb::UnknownFieldSet.MergeFrom(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(pb::CodedInputStream input) {
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, input);
            break;
          case 10: {
            Id = input.ReadString();
            break;
          }
          case 18: {
            Chat = input.ReadString();
            break;
          }
          case 26: {
            Message = input.ReadString();
            break;
          }
          case 34: {
            Key = input.ReadString();
            break;
          }
          case 42: {
            Signature = input.ReadString();
            break;
          }
          case 49: {
            Timestamp = input.ReadDouble();
            break;
          }
          case 58: {
            Sender = input.ReadString();
            break;
          }
          case 64: {
            Edited = input.ReadBool();
            break;
          }
          case 73: {
            EditedAt = input.ReadDouble();
            break;
          }
          case 82: {
            File = input.ReadString();
            break;
          }
        }
      }
    }

  }

  public sealed partial class MessageListResponse : pb::IMessage<MessageListResponse> {
    private static readonly pb::MessageParser<MessageListResponse> _parser = new pb::MessageParser<MessageListResponse>(() => new MessageListResponse());
    private pb::UnknownFieldSet _unknownFields;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pb::MessageParser<MessageListResponse> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::CryptChatProtos.Responses.Message.MessageReflection.Descriptor.MessageTypes[1]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public MessageListResponse() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public MessageListResponse(MessageListResponse other) : this() {
      messages_ = other.messages_.Clone();
      _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public MessageListResponse Clone() {
      return new MessageListResponse(this);
    }

    /// <summary>Field number for the "messages" field.</summary>
    public const int MessagesFieldNumber = 1;
    private static readonly pb::FieldCodec<global::CryptChatProtos.Responses.Message.MessageResponse> _repeated_messages_codec
        = pb::FieldCodec.ForMessage(10, global::CryptChatProtos.Responses.Message.MessageResponse.Parser);
    private readonly pbc::RepeatedField<global::CryptChatProtos.Responses.Message.MessageResponse> messages_ = new pbc::RepeatedField<global::CryptChatProtos.Responses.Message.MessageResponse>();
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public pbc::RepeatedField<global::CryptChatProtos.Responses.Message.MessageResponse> Messages {
      get { return messages_; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override bool Equals(object other) {
      return Equals(other as MessageListResponse);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool Equals(MessageListResponse other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if(!messages_.Equals(other.messages_)) return false;
      return Equals(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override int GetHashCode() {
      int hash = 1;
      hash ^= messages_.GetHashCode();
      if (_unknownFields != null) {
        hash ^= _unknownFields.GetHashCode();
      }
      return hash;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override string ToString() {
      return pb::JsonFormatter.ToDiagnosticString(this);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void WriteTo(pb::CodedOutputStream output) {
      messages_.WriteTo(output, _repeated_messages_codec);
      if (_unknownFields != null) {
        _unknownFields.WriteTo(output);
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int CalculateSize() {
      int size = 0;
      size += messages_.CalculateSize(_repeated_messages_codec);
      if (_unknownFields != null) {
        size += _unknownFields.CalculateSize();
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(MessageListResponse other) {
      if (other == null) {
        return;
      }
      messages_.Add(other.messages_);
      _unknownFields = pb::UnknownFieldSet.MergeFrom(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(pb::CodedInputStream input) {
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, input);
            break;
          case 10: {
            messages_.AddEntriesFrom(input, _repeated_messages_codec);
            break;
          }
        }
      }
    }

  }

  public sealed partial class SendMessageResponse : pb::IMessage<SendMessageResponse> {
    private static readonly pb::MessageParser<SendMessageResponse> _parser = new pb::MessageParser<SendMessageResponse>(() => new SendMessageResponse());
    private pb::UnknownFieldSet _unknownFields;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pb::MessageParser<SendMessageResponse> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::CryptChatProtos.Responses.Message.MessageReflection.Descriptor.MessageTypes[2]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public SendMessageResponse() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public SendMessageResponse(SendMessageResponse other) : this() {
      Id_ = other.Id_;
      chat_ = other.chat_;
      timestamp_ = other.timestamp_;
      _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public SendMessageResponse Clone() {
      return new SendMessageResponse(this);
    }

    /// <summary>Field number for the "_id" field.</summary>
    public const int IdFieldNumber = 1;
    private string Id_ = "";
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public string Id {
      get { return Id_; }
      set {
        Id_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    /// <summary>Field number for the "chat" field.</summary>
    public const int ChatFieldNumber = 2;
    private string chat_ = "";
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public string Chat {
      get { return chat_; }
      set {
        chat_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    /// <summary>Field number for the "timestamp" field.</summary>
    public const int TimestampFieldNumber = 3;
    private double timestamp_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public double Timestamp {
      get { return timestamp_; }
      set {
        timestamp_ = value;
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override bool Equals(object other) {
      return Equals(other as SendMessageResponse);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool Equals(SendMessageResponse other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (Id != other.Id) return false;
      if (Chat != other.Chat) return false;
      if (!pbc::ProtobufEqualityComparers.BitwiseDoubleEqualityComparer.Equals(Timestamp, other.Timestamp)) return false;
      return Equals(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override int GetHashCode() {
      int hash = 1;
      if (Id.Length != 0) hash ^= Id.GetHashCode();
      if (Chat.Length != 0) hash ^= Chat.GetHashCode();
      if (Timestamp != 0D) hash ^= pbc::ProtobufEqualityComparers.BitwiseDoubleEqualityComparer.GetHashCode(Timestamp);
      if (_unknownFields != null) {
        hash ^= _unknownFields.GetHashCode();
      }
      return hash;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override string ToString() {
      return pb::JsonFormatter.ToDiagnosticString(this);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void WriteTo(pb::CodedOutputStream output) {
      if (Id.Length != 0) {
        output.WriteRawTag(10);
        output.WriteString(Id);
      }
      if (Chat.Length != 0) {
        output.WriteRawTag(18);
        output.WriteString(Chat);
      }
      if (Timestamp != 0D) {
        output.WriteRawTag(25);
        output.WriteDouble(Timestamp);
      }
      if (_unknownFields != null) {
        _unknownFields.WriteTo(output);
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int CalculateSize() {
      int size = 0;
      if (Id.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(Id);
      }
      if (Chat.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(Chat);
      }
      if (Timestamp != 0D) {
        size += 1 + 8;
      }
      if (_unknownFields != null) {
        size += _unknownFields.CalculateSize();
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(SendMessageResponse other) {
      if (other == null) {
        return;
      }
      if (other.Id.Length != 0) {
        Id = other.Id;
      }
      if (other.Chat.Length != 0) {
        Chat = other.Chat;
      }
      if (other.Timestamp != 0D) {
        Timestamp = other.Timestamp;
      }
      _unknownFields = pb::UnknownFieldSet.MergeFrom(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(pb::CodedInputStream input) {
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, input);
            break;
          case 10: {
            Id = input.ReadString();
            break;
          }
          case 18: {
            Chat = input.ReadString();
            break;
          }
          case 25: {
            Timestamp = input.ReadDouble();
            break;
          }
        }
      }
    }

  }

  public sealed partial class EditMessageResponse : pb::IMessage<EditMessageResponse> {
    private static readonly pb::MessageParser<EditMessageResponse> _parser = new pb::MessageParser<EditMessageResponse>(() => new EditMessageResponse());
    private pb::UnknownFieldSet _unknownFields;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pb::MessageParser<EditMessageResponse> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::CryptChatProtos.Responses.Message.MessageReflection.Descriptor.MessageTypes[3]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public EditMessageResponse() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public EditMessageResponse(EditMessageResponse other) : this() {
      edited_ = other.edited_;
      editedAt_ = other.editedAt_;
      Id_ = other.Id_;
      _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public EditMessageResponse Clone() {
      return new EditMessageResponse(this);
    }

    /// <summary>Field number for the "edited" field.</summary>
    public const int EditedFieldNumber = 1;
    private bool edited_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool Edited {
      get { return edited_; }
      set {
        edited_ = value;
      }
    }

    /// <summary>Field number for the "edited_at" field.</summary>
    public const int EditedAtFieldNumber = 2;
    private double editedAt_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public double EditedAt {
      get { return editedAt_; }
      set {
        editedAt_ = value;
      }
    }

    /// <summary>Field number for the "_id" field.</summary>
    public const int IdFieldNumber = 3;
    private string Id_ = "";
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public string Id {
      get { return Id_; }
      set {
        Id_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override bool Equals(object other) {
      return Equals(other as EditMessageResponse);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool Equals(EditMessageResponse other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (Edited != other.Edited) return false;
      if (!pbc::ProtobufEqualityComparers.BitwiseDoubleEqualityComparer.Equals(EditedAt, other.EditedAt)) return false;
      if (Id != other.Id) return false;
      return Equals(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override int GetHashCode() {
      int hash = 1;
      if (Edited != false) hash ^= Edited.GetHashCode();
      if (EditedAt != 0D) hash ^= pbc::ProtobufEqualityComparers.BitwiseDoubleEqualityComparer.GetHashCode(EditedAt);
      if (Id.Length != 0) hash ^= Id.GetHashCode();
      if (_unknownFields != null) {
        hash ^= _unknownFields.GetHashCode();
      }
      return hash;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override string ToString() {
      return pb::JsonFormatter.ToDiagnosticString(this);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void WriteTo(pb::CodedOutputStream output) {
      if (Edited != false) {
        output.WriteRawTag(8);
        output.WriteBool(Edited);
      }
      if (EditedAt != 0D) {
        output.WriteRawTag(17);
        output.WriteDouble(EditedAt);
      }
      if (Id.Length != 0) {
        output.WriteRawTag(26);
        output.WriteString(Id);
      }
      if (_unknownFields != null) {
        _unknownFields.WriteTo(output);
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int CalculateSize() {
      int size = 0;
      if (Edited != false) {
        size += 1 + 1;
      }
      if (EditedAt != 0D) {
        size += 1 + 8;
      }
      if (Id.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(Id);
      }
      if (_unknownFields != null) {
        size += _unknownFields.CalculateSize();
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(EditMessageResponse other) {
      if (other == null) {
        return;
      }
      if (other.Edited != false) {
        Edited = other.Edited;
      }
      if (other.EditedAt != 0D) {
        EditedAt = other.EditedAt;
      }
      if (other.Id.Length != 0) {
        Id = other.Id;
      }
      _unknownFields = pb::UnknownFieldSet.MergeFrom(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(pb::CodedInputStream input) {
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, input);
            break;
          case 8: {
            Edited = input.ReadBool();
            break;
          }
          case 17: {
            EditedAt = input.ReadDouble();
            break;
          }
          case 26: {
            Id = input.ReadString();
            break;
          }
        }
      }
    }

  }

  public sealed partial class DeleteMessageResponse : pb::IMessage<DeleteMessageResponse> {
    private static readonly pb::MessageParser<DeleteMessageResponse> _parser = new pb::MessageParser<DeleteMessageResponse>(() => new DeleteMessageResponse());
    private pb::UnknownFieldSet _unknownFields;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pb::MessageParser<DeleteMessageResponse> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::CryptChatProtos.Responses.Message.MessageReflection.Descriptor.MessageTypes[4]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public DeleteMessageResponse() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public DeleteMessageResponse(DeleteMessageResponse other) : this() {
      status_ = other.status_;
      _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public DeleteMessageResponse Clone() {
      return new DeleteMessageResponse(this);
    }

    /// <summary>Field number for the "status" field.</summary>
    public const int StatusFieldNumber = 1;
    private string status_ = "";
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public string Status {
      get { return status_; }
      set {
        status_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override bool Equals(object other) {
      return Equals(other as DeleteMessageResponse);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool Equals(DeleteMessageResponse other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (Status != other.Status) return false;
      return Equals(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override int GetHashCode() {
      int hash = 1;
      if (Status.Length != 0) hash ^= Status.GetHashCode();
      if (_unknownFields != null) {
        hash ^= _unknownFields.GetHashCode();
      }
      return hash;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override string ToString() {
      return pb::JsonFormatter.ToDiagnosticString(this);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void WriteTo(pb::CodedOutputStream output) {
      if (Status.Length != 0) {
        output.WriteRawTag(10);
        output.WriteString(Status);
      }
      if (_unknownFields != null) {
        _unknownFields.WriteTo(output);
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int CalculateSize() {
      int size = 0;
      if (Status.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(Status);
      }
      if (_unknownFields != null) {
        size += _unknownFields.CalculateSize();
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(DeleteMessageResponse other) {
      if (other == null) {
        return;
      }
      if (other.Status.Length != 0) {
        Status = other.Status;
      }
      _unknownFields = pb::UnknownFieldSet.MergeFrom(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(pb::CodedInputStream input) {
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, input);
            break;
          case 10: {
            Status = input.ReadString();
            break;
          }
        }
      }
    }

  }

  #endregion

}

#endregion Designer generated code
