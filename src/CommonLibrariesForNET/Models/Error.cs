﻿namespace Salesforce.Common.Models
{
    public enum Error
    {
        Unknown,
        InvalidClient,
        UnsupportedGrantType,
        InvalidGrant,
        AuthenticationFailure,
        InvalidPassword,
        ClientIdentifierInvalid,
        NotFound,
        MalformedQuery,
        FieldCustomValidationException,
        InvalidFieldForInsertUpdate,
        InvalidClientId,
        InvalidField,
        RequiredFieldMissing,
        StringTooLong,
        EntityIsDeleted,
        FieldIntegrityException,
        ApiCurrentlyDisabled,
        ApiDisabledForOrg,
        CantAddStandardPortalUserToTerritory,
        CircularObjectGraph,
        ClientNotAccessibleForUser,
        ClientRequiredUpdateForUser,
        DeleteRequiredOnCascade,
        DuplicateCommNickname,
        EmailBatchSizeLimitExceeded,
        EmailToCaseInvalidRouting,
        EmailToCaseLimitExceeded,
        EmailToCaseNotEnabled,
        ExceededIdLimit,
        ExceededLeadConvertLimit,
        ExceededMaxSizeRequest,
        ExceededMaxTypesLimit,
        ExceededQuota,
        FunctionalityNotEnabled,
        InactiveOwnerOrUser,
        InactivePortal,
        InsufficientAccess,
        InvalidAssignmentRule,
        InvalidBatchSize,
        InvalidCrossReferenceKey,
        InvalidFilterLanguage,
        InvalidFilterValue,
        InvalidIdField,
        InvalidGoogleDocsUrl,
        InvalidLocator,
        InvalidLogin,
        InvalidNewPassword,
        InvalidOperation,
        InvalidOperationWithExpiredPassword,
        InvalidQueryFilterOperator,
        InvalidQueryLocator,
        InvalidQueryScope,
        InvalidReplicationDate,
        InvalidSetupOwner,
        InvalidSearch,
        InvalidSearchScope,
        InvalidSessionId,
        InvalidSoapHeader,
        InvalidSsoGatewayUrl,
        InvalidType,
        InvalidTypeForOperation,
        LimitExceeded,
        LoginDuringRestrictedDomain,
        LoginDUringRestrictedTime,
        MalformedId,
        MalformedSearch,
        MissingArgument,
        MixedDmlOperation,
        NotModified,
        NoSoftphoneLayout,
        NumberOutsideValidRange,
        OperationTooLarge,
        OrgLocked,
        OrgNotOwnedByInstance,
        PasswordLockout,
        PortalNoAccess,
        QueryTimeout,
        QueryTooComplicated,
        RequestLimitExceeded,
        RequestRunningTooLong,
        ServerUnavailable,
        SsoServiceDown,
        TooManyApexRequests,
        TrialExpired,
        UnsupportedApiVersion,
        UnsupportedClient,
        AllOrNoneOperationRolledBack,
        AlreadyInProcess,
        AssigneeTypeRequired,
        BadCustomEntityParentDomain,
        BccNotAllowedIfBccComplianceEnabled,
        BccSelfNotAllowedIfBccComplianceEnabled,
        CannotCascadeProductActive,
        CannotChangeFileTypeOfApexReferencedField,
        CannotCreateAnotherManagedPackage,
        CannotDeactivateDivision,
        CannotDeleteLastDatedConversionRate,
        CannotDeleteManagedObject,
        CannotDisableLastAdmin,
        CannotEnableIpRestrictedRequests,
        CannotInsertUpdateActivateEntity,
        CannotModifyManagedObject,
        CannotRenameApexReferencedField,
        CannotRenameApexReferencedObject,
        CannotReparentRecord,
        CannotResolveName,
        CannotUpdateConvertedLead,
        CannotDisableCorpCurrency,
        CanntUnsetCorpCurrency,
        ChildShareFailsParent,
        CircularDependency,
        CommunityNotAccessible,
        CustomClobFieldLimitExceeded,
        CustomEntityOrFieldLimit,
        CustomFieldIndexLimitExceeded,
        CustomIndexExists,
        CustomLinkLimitExceeded,
        CustomTabLimitExceeded,
        DeleteFailed,
        DependencyExists,
        DuplicateCaseSolution,
        DuplicateCustomEntityDefinition,
        DuplicateCustomTabMotif,
        DuplicateDeveloperName,
        DuplicatesDetected,
        DuplicateExternalId,
        DuplicateMasterLabel,
        DuplicateSenderDisplayName,
        DuplicateUsername,
        DuplicateValue,
        EmailAddressBounced,
        EmailNotProcessedDueToPriorError,
        EmailOptedOut,
        EmailTemplateFormulaError,
        EmailTemplateMergefieldAccessError,
        EmailTemplateMergefieldError,
        EmailTemplateMergaefieldValueError,
        EmailTemplateProcessingError,
        EmptyScontrolFileName,
        EntityFailedIflastmodifiedOnUpdate,
        EntityIsArchived,
        EnvironmentHubMembershipConflict,
        ErrorInMailer,
        FailedActivation,
        FieldFilterValidationException,
        FilteredLookupLimitExceeded,
        HtmlFileUploadNotAllowed,
        ImageTooLarge,
        InsertUpdateDeleteNotAllowedDuringMaintance,
        InsufficientAccessOnCrossReferenceEntity,
        InsufficientAccessOrReadonly,
        InvalidAccessLevel,
        InvalidArgumentType,
        InvalidAssigneeType,
        InvalidBatchOperation,
        InvalidContentType,
        InvalidCreditCardInfo,
        InvalidCrossReferenceTypeForField,
        InvalidCurrencyConvRate,
        InvalidCurrencyCorpRate,
        InvalidCurrencyIso,
        InvalidEmailAddress,
        InvalidEmptyKeyOwner,
        InvalidEventSubscription,
        InvalidFieldWhenUsingTemplate,
        InvalidFilterAction,
        InvalidInetAddress,
        InvalidLineItemCloneState,
        InvalidMasterOrTranslatedSolution,
        InvalidMessageIdReference,
        InvalidOperator,
        InvalidOrNullForRestrictedPicklist,
        InvalidPartnerNetworkStatus,
        InvalidPersonAccountOperation,
        InvalidReadOnlyUserDml,
        InvalidSaveAsActivityFlag,
        InvalidStatus,
        InvalidTypeOnFieldInRecord,
        IpRangeLimitExceeded,
        JigsawImportLimitExceeded,
        LicenseLimitExceeded,
        LightPortalUserException,
        LoginChallengeIssued,
        LoginChallengePending,
        LoginMustUseSecurityToken,
        ManagerNotDefined,
        MassmailRetryLimitExceeded,
        MaximumCcemailsExceeded,
        MaximumDashboardComponentsExceeded,
        MaximumHierarchyLevelsReached,
        MaximumSizeOfAttachment,
        MaximumSizeOfDocument,
        MaxActionsPerRuleExceeded,
        MaxActiveRulesExceeded,
        MaxApprovalStepsExceeded,
        MaxFormulasPerRuleExceeded,
        MaxRulesExceeded,
        MaxRuleEntriesExceeded,
        MaxTaskDescriptionExceeded,
        MaxTmRulesExceeded,
        MaxTmRuleItemsExceeded,
        MergeFailed,
        NonuniqueShippingAddress,
        NoApplicableProcess,
        NoAttachmentPermission,
        NoInactiveDivisonMembers,
        NoMassMailPermission,
        NumHistoryFieldsBySobjectExceeded,
        OpWithInvalidUserTypeException,
        OptedOutOfMassMail,
        PackageLicenseRequired,
        PortalUserAlreadyExistsForContact,
        PrivateContactOnAsset,
        RecordInUseByWorkflow,
        SelfRefenceFromTrigger,
        ShareNeededForChildOwner,
        SingleEmailLimitExceeded,
        StandardPriceNotDefined,
        StorageLimitExceeded,
        TabsetLimitExceeded,
        TemplateNotActive,
        TerritoryRealignInProcess,
        TextDataOutsideSupportedCharset,
        TooManyEnumValue,
        TransferRequiresRead,
        UnableToLockRow,
        UnavailableRecordTypeException,
        UndeleteFailed,
        UnknownException,
        UnspecifiedEmailAddress,
        UnsupportedApexTriggerOperation,
        UnverifiedSenderAddress,
        WeblinkSizeLimitExceeded,
        WrongControllerType,
        NonJsonErrorResponse,
        WeblinkUrlInvalid
    }
}
