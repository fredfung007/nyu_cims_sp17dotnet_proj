using DataAccessLayer.Constants;
using System;

namespace BusinessLogic.Helpers
{
    // Name Converter for U.S. States.
    public class StateConverter : IStateConverter
    {
        public string GetState(UsState state)
        {
            switch (state)
            {
                case UsState.Al:
                    return "ALABAMA";

                case UsState.Ak:
                    return "ALASKA";

                case UsState.As:
                    return "AMERICAN SAMOA";

                case UsState.Az:
                    return "ARIZONA";

                case UsState.Ar:
                    return "ARKANSAS";

                case UsState.Ca:
                    return "CALIFORNIA";

                case UsState.Co:
                    return "COLORADO";

                case UsState.Ct:
                    return "CONNECTICUT";

                case UsState.De:
                    return "DELAWARE";

                case UsState.Dc:
                    return "DISTRICT OF COLUMBIA";

                case UsState.Fm:
                    return "FEDERATED STATES OF MICRONESIA";

                case UsState.Fl:
                    return "FLORIDA";

                case UsState.Ga:
                    return "GEORGIA";

                case UsState.Gu:
                    return "GUAM";

                case UsState.Hi:
                    return "HAWAII";

                case UsState.Id:
                    return "IDAHO";

                case UsState.Il:
                    return "ILLINOIS";

                case UsState.In:
                    return "INDIANA";

                case UsState.Ia:
                    return "IOWA";

                case UsState.Ks:
                    return "KANSAS";

                case UsState.Ky:
                    return "KENTUCKY";

                case UsState.La:
                    return "LOUISIANA";

                case UsState.Me:
                    return "MAINE";

                case UsState.Mh:
                    return "MARSHALL ISLANDS";

                case UsState.Md:
                    return "MARYLAND";

                case UsState.Ma:
                    return "MASSACHUSETTS";

                case UsState.Mi:
                    return "MICHIGAN";

                case UsState.Mn:
                    return "MINNESOTA";

                case UsState.Ms:
                    return "MISSISSIPPI";

                case UsState.Mo:
                    return "MISSOURI";

                case UsState.Mt:
                    return "MONTANA";

                case UsState.Ne:
                    return "NEBRASKA";

                case UsState.Nv:
                    return "NEVADA";

                case UsState.Nh:
                    return "NEW HAMPSHIRE";

                case UsState.Nj:
                    return "NEW JERSEY";

                case UsState.Nm:
                    return "NEW MEXICO";

                case UsState.Ny:
                    return "NEW YORK";

                case UsState.Nc:
                    return "NORTH CAROLINA";

                case UsState.Nd:
                    return "NORTH DAKOTA";

                case UsState.Mp:
                    return "NORTHERN MARIANA ISLANDS";

                case UsState.Oh:
                    return "OHIO";

                case UsState.Ok:
                    return "OKLAHOMA";

                case UsState.Or:
                    return "OREGON";

                case UsState.Pw:
                    return "PALAU";

                case UsState.Pa:
                    return "PENNSYLVANIA";

                case UsState.Pr:
                    return "PUERTO RICO";

                case UsState.Ri:
                    return "RHODE ISLAND";

                case UsState.Sc:
                    return "SOUTH CAROLINA";

                case UsState.Sd:
                    return "SOUTH DAKOTA";

                case UsState.Tn:
                    return "TENNESSEE";

                case UsState.Tx:
                    return "TEXAS";

                case UsState.Ut:
                    return "UTAH";

                case UsState.Vt:
                    return "VERMONT";

                case UsState.Vi:
                    return "VIRGIN ISLANDS";

                case UsState.Va:
                    return "VIRGINIA";

                case UsState.Wa:
                    return "WASHINGTON";

                case UsState.Wv:
                    return "WEST VIRGINIA";

                case UsState.Wi:
                    return "WISCONSIN";

                case UsState.Wy:
                    return "WYOMING";
            }

            throw new Exception("Not Available");
        }

        public UsState GetStateByName(string name)
        {
            switch (name.ToUpper())
            {
                case "ALABAMA":
                    return UsState.Al;

                case "ALASKA":
                    return UsState.Ak;

                case "AMERICAN SAMOA":
                    return UsState.As;

                case "ARIZONA":
                    return UsState.Az;

                case "ARKANSAS":
                    return UsState.Ar;

                case "CALIFORNIA":
                    return UsState.Ca;

                case "COLORADO":
                    return UsState.Co;

                case "CONNECTICUT":
                    return UsState.Ct;

                case "DELAWARE":
                    return UsState.De;

                case "DISTRICT OF COLUMBIA":
                    return UsState.Dc;

                case "FEDERATED STATES OF MICRONESIA":
                    return UsState.Fm;

                case "FLORIDA":
                    return UsState.Fl;

                case "GEORGIA":
                    return UsState.Ga;

                case "GUAM":
                    return UsState.Gu;

                case "HAWAII":
                    return UsState.Hi;

                case "IDAHO":
                    return UsState.Id;

                case "ILLINOIS":
                    return UsState.Il;

                case "INDIANA":
                    return UsState.In;

                case "IOWA":
                    return UsState.Ia;

                case "KANSAS":
                    return UsState.Ks;

                case "KENTUCKY":
                    return UsState.Ky;

                case "LOUISIANA":
                    return UsState.La;

                case "MAINE":
                    return UsState.Me;

                case "MARSHALL ISLANDS":
                    return UsState.Mh;

                case "MARYLAND":
                    return UsState.Md;

                case "MASSACHUSETTS":
                    return UsState.Ma;

                case "MICHIGAN":
                    return UsState.Mi;

                case "MINNESOTA":
                    return UsState.Mn;

                case "MISSISSIPPI":
                    return UsState.Ms;

                case "MISSOURI":
                    return UsState.Mo;

                case "MONTANA":
                    return UsState.Mt;

                case "NEBRASKA":
                    return UsState.Ne;

                case "NEVADA":
                    return UsState.Nv;

                case "NEW HAMPSHIRE":
                    return UsState.Nh;

                case "NEW JERSEY":
                    return UsState.Nj;

                case "NEW MEXICO":
                    return UsState.Nm;

                case "NEW YORK":
                    return UsState.Ny;

                case "NORTH CAROLINA":
                    return UsState.Nc;

                case "NORTH DAKOTA":
                    return UsState.Nd;

                case "NORTHERN MARIANA ISLANDS":
                    return UsState.Mp;

                case "OHIO":
                    return UsState.Oh;

                case "OKLAHOMA":
                    return UsState.Ok;

                case "OREGON":
                    return UsState.Or;

                case "PALAU":
                    return UsState.Pw;

                case "PENNSYLVANIA":
                    return UsState.Pa;

                case "PUERTO RICO":
                    return UsState.Pr;

                case "RHODE ISLAND":
                    return UsState.Ri;

                case "SOUTH CAROLINA":
                    return UsState.Sc;

                case "SOUTH DAKOTA":
                    return UsState.Sd;

                case "TENNESSEE":
                    return UsState.Tn;

                case "TEXAS":
                    return UsState.Tx;

                case "UTAH":
                    return UsState.Ut;

                case "VERMONT":
                    return UsState.Vt;

                case "VIRGIN ISLANDS":
                    return UsState.Vi;

                case "VIRGINIA":
                    return UsState.Va;

                case "WASHINGTON":
                    return UsState.Wa;

                case "WEST VIRGINIA":
                    return UsState.Wv;

                case "WISCONSIN":
                    return UsState.Wi;

                case "WYOMING":
                    return UsState.Wy;
            }

            throw new Exception("Not Available");
        }
    }
}
