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
                case US_STATE.Al:
                    return "ALABAMA";

                case US_STATE.Ak:
                    return "ALASKA";

                case US_STATE.As:
                    return "AMERICAN SAMOA";

                case US_STATE.Az:
                    return "ARIZONA";

                case US_STATE.Ar:
                    return "ARKANSAS";

                case US_STATE.Ca:
                    return "CALIFORNIA";

                case US_STATE.Co:
                    return "COLORADO";

                case US_STATE.Ct:
                    return "CONNECTICUT";

                case US_STATE.De:
                    return "DELAWARE";

                case US_STATE.Dc:
                    return "DISTRICT OF COLUMBIA";

                case US_STATE.Fm:
                    return "FEDERATED STATES OF MICRONESIA";

                case US_STATE.Fl:
                    return "FLORIDA";

                case US_STATE.Ga:
                    return "GEORGIA";

                case US_STATE.Gu:
                    return "GUAM";

                case US_STATE.Hi:
                    return "HAWAII";

                case US_STATE.Id:
                    return "IDAHO";

                case US_STATE.Il:
                    return "ILLINOIS";

                case US_STATE.In:
                    return "INDIANA";

                case US_STATE.Ia:
                    return "IOWA";

                case US_STATE.Ks:
                    return "KANSAS";

                case US_STATE.Ky:
                    return "KENTUCKY";

                case US_STATE.La:
                    return "LOUISIANA";

                case US_STATE.Me:
                    return "MAINE";

                case US_STATE.Mh:
                    return "MARSHALL ISLANDS";

                case US_STATE.Md:
                    return "MARYLAND";

                case US_STATE.Ma:
                    return "MASSACHUSETTS";

                case US_STATE.Mi:
                    return "MICHIGAN";

                case US_STATE.Mn:
                    return "MINNESOTA";

                case US_STATE.Ms:
                    return "MISSISSIPPI";

                case US_STATE.Mo:
                    return "MISSOURI";

                case US_STATE.Mt:
                    return "MONTANA";

                case US_STATE.Ne:
                    return "NEBRASKA";

                case US_STATE.Nv:
                    return "NEVADA";

                case US_STATE.Nh:
                    return "NEW HAMPSHIRE";

                case US_STATE.Nj:
                    return "NEW JERSEY";

                case US_STATE.Nm:
                    return "NEW MEXICO";

                case US_STATE.Ny:
                    return "NEW YORK";

                case US_STATE.Nc:
                    return "NORTH CAROLINA";

                case US_STATE.Nd:
                    return "NORTH DAKOTA";

                case US_STATE.Mp:
                    return "NORTHERN MARIANA ISLANDS";

                case US_STATE.Oh:
                    return "OHIO";

                case US_STATE.Ok:
                    return "OKLAHOMA";

                case US_STATE.Or:
                    return "OREGON";

                case US_STATE.Pw:
                    return "PALAU";

                case US_STATE.Pa:
                    return "PENNSYLVANIA";

                case US_STATE.Pr:
                    return "PUERTO RICO";

                case US_STATE.Ri:
                    return "RHODE ISLAND";

                case US_STATE.Sc:
                    return "SOUTH CAROLINA";

                case US_STATE.Sd:
                    return "SOUTH DAKOTA";

                case US_STATE.Tn:
                    return "TENNESSEE";

                case US_STATE.Tx:
                    return "TEXAS";

                case US_STATE.Ut:
                    return "UTAH";

                case US_STATE.Vt:
                    return "VERMONT";

                case US_STATE.Vi:
                    return "VIRGIN ISLANDS";

                case US_STATE.Va:
                    return "VIRGINIA";

                case US_STATE.Wa:
                    return "WASHINGTON";

                case US_STATE.Wv:
                    return "WEST VIRGINIA";

                case US_STATE.Wi:
                    return "WISCONSIN";

                case US_STATE.Wy:
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
                    return US_STATE.Al;

                case "ALASKA":
                    return US_STATE.Ak;

                case "AMERICAN SAMOA":
                    return US_STATE.As;

                case "ARIZONA":
                    return US_STATE.Az;

                case "ARKANSAS":
                    return US_STATE.Ar;

                case "CALIFORNIA":
                    return US_STATE.Ca;

                case "COLORADO":
                    return US_STATE.Co;

                case "CONNECTICUT":
                    return US_STATE.Ct;

                case "DELAWARE":
                    return US_STATE.De;

                case "DISTRICT OF COLUMBIA":
                    return US_STATE.Dc;

                case "FEDERATED STATES OF MICRONESIA":
                    return US_STATE.Fm;

                case "FLORIDA":
                    return US_STATE.Fl;

                case "GEORGIA":
                    return US_STATE.Ga;

                case "GUAM":
                    return US_STATE.Gu;

                case "HAWAII":
                    return US_STATE.Hi;

                case "IDAHO":
                    return US_STATE.Id;

                case "ILLINOIS":
                    return US_STATE.Il;

                case "INDIANA":
                    return US_STATE.In;

                case "IOWA":
                    return US_STATE.Ia;

                case "KANSAS":
                    return US_STATE.Ks;

                case "KENTUCKY":
                    return US_STATE.Ky;

                case "LOUISIANA":
                    return US_STATE.La;

                case "MAINE":
                    return US_STATE.Me;

                case "MARSHALL ISLANDS":
                    return US_STATE.Mh;

                case "MARYLAND":
                    return US_STATE.Md;

                case "MASSACHUSETTS":
                    return US_STATE.Ma;

                case "MICHIGAN":
                    return US_STATE.Mi;

                case "MINNESOTA":
                    return US_STATE.Mn;

                case "MISSISSIPPI":
                    return US_STATE.Ms;

                case "MISSOURI":
                    return US_STATE.Mo;

                case "MONTANA":
                    return US_STATE.Mt;

                case "NEBRASKA":
                    return US_STATE.Ne;

                case "NEVADA":
                    return US_STATE.Nv;

                case "NEW HAMPSHIRE":
                    return US_STATE.Nh;

                case "NEW JERSEY":
                    return US_STATE.Nj;

                case "NEW MEXICO":
                    return US_STATE.Nm;

                case "NEW YORK":
                    return US_STATE.Ny;

                case "NORTH CAROLINA":
                    return US_STATE.Nc;

                case "NORTH DAKOTA":
                    return US_STATE.Nd;

                case "NORTHERN MARIANA ISLANDS":
                    return US_STATE.Mp;

                case "OHIO":
                    return US_STATE.Oh;

                case "OKLAHOMA":
                    return US_STATE.Ok;

                case "OREGON":
                    return US_STATE.Or;

                case "PALAU":
                    return US_STATE.Pw;

                case "PENNSYLVANIA":
                    return US_STATE.Pa;

                case "PUERTO RICO":
                    return US_STATE.Pr;

                case "RHODE ISLAND":
                    return US_STATE.Ri;

                case "SOUTH CAROLINA":
                    return US_STATE.Sc;

                case "SOUTH DAKOTA":
                    return US_STATE.Sd;

                case "TENNESSEE":
                    return US_STATE.Tn;

                case "TEXAS":
                    return US_STATE.Tx;

                case "UTAH":
                    return US_STATE.Ut;

                case "VERMONT":
                    return US_STATE.Vt;

                case "VIRGIN ISLANDS":
                    return US_STATE.Vi;

                case "VIRGINIA":
                    return US_STATE.Va;

                case "WASHINGTON":
                    return US_STATE.Wa;

                case "WEST VIRGINIA":
                    return US_STATE.Wv;

                case "WISCONSIN":
                    return US_STATE.Wi;

                case "WYOMING":
                    return US_STATE.Wy;
                default:
                    throw new KeyNotFoundException("Not Available");
            }
        }
    }
}
