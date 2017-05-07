using DataAccessLayer.Constants;
using System;
using System.Collections.Generic;

namespace BusinessLogic.Helpers
{
    // Name Converter for U.S. States.
    public static class StateConverter
    {
        public static string GetState(US_STATE state)
        {
            switch (state)
            {
                case US_STATE.AL:
                    return "ALABAMA";

                case US_STATE.AK:
                    return "ALASKA";

                case US_STATE.AS:
                    return "AMERICAN SAMOA";

                case US_STATE.AZ:
                    return "ARIZONA";

                case US_STATE.AR:
                    return "ARKANSAS";

                case US_STATE.CA:
                    return "CALIFORNIA";

                case US_STATE.CO:
                    return "COLORADO";

                case US_STATE.CT:
                    return "CONNECTICUT";

                case US_STATE.DE:
                    return "DELAWARE";

                case US_STATE.DC:
                    return "DISTRICT OF COLUMBIA";

                case US_STATE.FM:
                    return "FEDERATED STATES OF MICRONESIA";

                case US_STATE.FL:
                    return "FLORIDA";

                case US_STATE.GA:
                    return "GEORGIA";

                case US_STATE.GU:
                    return "GUAM";

                case US_STATE.HI:
                    return "HAWAII";

                case US_STATE.ID:
                    return "IDAHO";

                case US_STATE.IL:
                    return "ILLINOIS";

                case US_STATE.IN:
                    return "INDIANA";

                case US_STATE.IA:
                    return "IOWA";

                case US_STATE.KS:
                    return "KANSAS";

                case US_STATE.KY:
                    return "KENTUCKY";

                case US_STATE.LA:
                    return "LOUISIANA";

                case US_STATE.ME:
                    return "MAINE";

                case US_STATE.MH:
                    return "MARSHALL ISLANDS";

                case US_STATE.MD:
                    return "MARYLAND";

                case US_STATE.MA:
                    return "MASSACHUSETTS";

                case US_STATE.MI:
                    return "MICHIGAN";

                case US_STATE.MN:
                    return "MINNESOTA";

                case US_STATE.MS:
                    return "MISSISSIPPI";

                case US_STATE.MO:
                    return "MISSOURI";

                case US_STATE.MT:
                    return "MONTANA";

                case US_STATE.NE:
                    return "NEBRASKA";

                case US_STATE.NV:
                    return "NEVADA";

                case US_STATE.NH:
                    return "NEW HAMPSHIRE";

                case US_STATE.NJ:
                    return "NEW JERSEY";

                case US_STATE.NM:
                    return "NEW MEXICO";

                case US_STATE.NY:
                    return "NEW YORK";

                case US_STATE.NC:
                    return "NORTH CAROLINA";

                case US_STATE.ND:
                    return "NORTH DAKOTA";

                case US_STATE.MP:
                    return "NORTHERN MARIANA ISLANDS";

                case US_STATE.OH:
                    return "OHIO";

                case US_STATE.OK:
                    return "OKLAHOMA";

                case US_STATE.OR:
                    return "OREGON";

                case US_STATE.PW:
                    return "PALAU";

                case US_STATE.PA:
                    return "PENNSYLVANIA";

                case US_STATE.PR:
                    return "PUERTO RICO";

                case US_STATE.RI:
                    return "RHODE ISLAND";

                case US_STATE.SC:
                    return "SOUTH CAROLINA";

                case US_STATE.SD:
                    return "SOUTH DAKOTA";

                case US_STATE.TN:
                    return "TENNESSEE";

                case US_STATE.TX:
                    return "TEXAS";

                case US_STATE.UT:
                    return "UTAH";

                case US_STATE.VT:
                    return "VERMONT";

                case US_STATE.VI:
                    return "VIRGIN ISLANDS";

                case US_STATE.VA:
                    return "VIRGINIA";

                case US_STATE.WA:
                    return "WASHINGTON";

                case US_STATE.WV:
                    return "WEST VIRGINIA";

                case US_STATE.WI:
                    return "WISCONSIN";

                case US_STATE.WY:
                    return "WYOMING";
                default:
                    throw new KeyNotFoundException("NO SUCH STATE FOUND");
            }
        }

        public static US_STATE GetStateByName(string name)
        {
            switch (name.ToUpper())
            {
                case "ALABAMA":
                    return US_STATE.AL;

                case "ALASKA":
                    return US_STATE.AK;

                case "AMERICAN SAMOA":
                    return US_STATE.AS;

                case "ARIZONA":
                    return US_STATE.AZ;

                case "ARKANSAS":
                    return US_STATE.AR;

                case "CALIFORNIA":
                    return US_STATE.CA;

                case "COLORADO":
                    return US_STATE.CO;

                case "CONNECTICUT":
                    return US_STATE.CT;

                case "DELAWARE":
                    return US_STATE.DE;

                case "DISTRICT OF COLUMBIA":
                    return US_STATE.DC;

                case "FEDERATED STATES OF MICRONESIA":
                    return US_STATE.FM;

                case "FLORIDA":
                    return US_STATE.FL;

                case "GEORGIA":
                    return US_STATE.GA;

                case "GUAM":
                    return US_STATE.GU;

                case "HAWAII":
                    return US_STATE.HI;

                case "IDAHO":
                    return US_STATE.ID;

                case "ILLINOIS":
                    return US_STATE.IL;

                case "INDIANA":
                    return US_STATE.IN;

                case "IOWA":
                    return US_STATE.IA;

                case "KANSAS":
                    return US_STATE.KS;

                case "KENTUCKY":
                    return US_STATE.KY;

                case "LOUISIANA":
                    return US_STATE.LA;

                case "MAINE":
                    return US_STATE.ME;

                case "MARSHALL ISLANDS":
                    return US_STATE.MH;

                case "MARYLAND":
                    return US_STATE.MD;

                case "MASSACHUSETTS":
                    return US_STATE.MA;

                case "MICHIGAN":
                    return US_STATE.MI;

                case "MINNESOTA":
                    return US_STATE.MN;

                case "MISSISSIPPI":
                    return US_STATE.MS;

                case "MISSOURI":
                    return US_STATE.MO;

                case "MONTANA":
                    return US_STATE.MT;

                case "NEBRASKA":
                    return US_STATE.NE;

                case "NEVADA":
                    return US_STATE.NV;

                case "NEW HAMPSHIRE":
                    return US_STATE.NH;

                case "NEW JERSEY":
                    return US_STATE.NJ;

                case "NEW MEXICO":
                    return US_STATE.NM;

                case "NEW YORK":
                    return US_STATE.NY;

                case "NORTH CAROLINA":
                    return US_STATE.NC;

                case "NORTH DAKOTA":
                    return US_STATE.ND;

                case "NORTHERN MARIANA ISLANDS":
                    return US_STATE.MP;

                case "OHIO":
                    return US_STATE.OH;

                case "OKLAHOMA":
                    return US_STATE.OK;

                case "OREGON":
                    return US_STATE.OR;

                case "PALAU":
                    return US_STATE.PW;

                case "PENNSYLVANIA":
                    return US_STATE.PA;

                case "PUERTO RICO":
                    return US_STATE.PR;

                case "RHODE ISLAND":
                    return US_STATE.RI;

                case "SOUTH CAROLINA":
                    return US_STATE.SC;

                case "SOUTH DAKOTA":
                    return US_STATE.SD;

                case "TENNESSEE":
                    return US_STATE.TN;

                case "TEXAS":
                    return US_STATE.TX;

                case "UTAH":
                    return US_STATE.UT;

                case "VERMONT":
                    return US_STATE.VT;

                case "VIRGIN ISLANDS":
                    return US_STATE.VI;

                case "VIRGINIA":
                    return US_STATE.VA;

                case "WASHINGTON":
                    return US_STATE.WA;

                case "WEST VIRGINIA":
                    return US_STATE.WV;

                case "WISCONSIN":
                    return US_STATE.WI;

                case "WYOMING":
                    return US_STATE.WY;
                default:
                    throw new KeyNotFoundException("Not Available");
            }
        }
    }
}
